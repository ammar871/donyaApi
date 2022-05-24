using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Helpers;
using Commander.Models;
using Commander.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Commander.Controllers
{
    [Route("api")]
    [ApiController]
    public class AddsController : ControllerBase
    {


        IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IAddRepo _repository;
        private readonly CommanderContext _context;
        private IMapper _mapper;
        public AddsController(IHttpContextAccessor httpContextAccessor, CommanderContext context, IWebHostEnvironment hostingEnvironment, IAddRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        [Route("get-adds")]
        public async Task<ActionResult> GetAll([FromForm] int categoryId, [FromForm] string cuntry)
        {
            if (cuntry == null)
            {

                var data = await _context.Adds.Where(p => p.CategoryId == categoryId && p.Status == 1).ToListAsync();
                return Ok(data);
            }
            else
            {
                var data = await _context.Adds.Where(p => p.CategoryId == categoryId && p.Status == 1 && p.Country == cuntry).ToListAsync();
                return Ok(data);
            }

        }


        [HttpGet]
        [Route("get-adds-home")]
        public async Task<ActionResult> GetAddsHome()
        {
            var currentData = await _context.Adds.Where(x => x.Status == 1).ToListAsync();
            var spoonData = await _context.Adds.Where(x => x.Status == 0).ToListAsync();
            var unacceptableAdds = await _context.Adds.Where(x => x.Status == 2).ToListAsync();
            ResponseAdds responseAdds = new ResponseAdds
            {
                CurrentAdds = currentData,
                SpoonAdds = spoonData,
                UnacceptableAdds = unacceptableAdds,
            };
            return Ok(responseAdds);
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("get-my-adds")]
        public async Task<ActionResult> GetMyAdds()
        {
            User user = await Functions.getCurrentUser(_httpContextAccessor, _context);

            var currentData = await _context.Adds.Where(x => x.Status == 1 && x.UserId == user.Id).ToListAsync();
            var spoonData = await _context.Adds.Where(x => x.Status == 0 && x.UserId == user.Id).ToListAsync();
            var unacceptableAdds = await _context.Adds.Where(x => x.Status == 2 && x.UserId == user.Id).ToListAsync();
            ResponseAdds responseAdds = new ResponseAdds
            {
                CurrentAdds = currentData,
                SpoonAdds = spoonData,
                UnacceptableAdds = unacceptableAdds,
            };
            return Ok(responseAdds);
        }


        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("searsh-my-adds")]
        public async Task<ActionResult> SearchMyAdds([FromForm] string nameAdd, [FromForm] string cuntry)
        {
            User user = await Functions.getCurrentUser(_httpContextAccessor, _context);
            List<Adds> data = new List<Adds>();

            if (cuntry == null)
            {
                data = await _context.Adds.Where(x => x.Status == 1 && x.UserId == user.Id && x.Title == nameAdd).ToListAsync();

                return Ok(data);
            }
            else
            {

                data = await _context.Adds.Where(x => x.Status == 1 && x.UserId == user.Id && x.Title == nameAdd && x.Country == cuntry).ToListAsync();

                return Ok(data);
            }


        }



        [HttpGet]
        [Route("get-counters")]
        public ActionResult<General> GetCounters()
        {
            var commandItems = _repository.GetCounter();
            return Ok(commandItems);
        }




        [HttpGet]
        [Route("adds-By-CategId")]
        public ActionResult<IEnumerable<Adds>> GetAddsByCategoryID([FromForm] int id)
        {
            var commandItems = _repository.GetAddsByCategoryId(id);


            return Ok(commandItems);


        }



        [HttpPost]
        [Route("image/upload")]
        public ActionResult uploadImage([FromForm] IFormFile file)
        {
            string path = _hostingEnvironment.WebRootPath + "/images/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            String fileName = DateTime.Now.ToString("yyyyMMddTHHmmss") + ".jpeg";
            using (var fileStream = System.IO.File.Create(path + fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
                return Ok(fileName);
            }
        }


        [Authorize(Roles = "user")]
        [HttpPost("add-adds")]

        public async Task<ActionResult> CreateAdds([FromForm] Adds add)


        {


            User user = await Functions.getCurrentUser(_httpContextAccessor, _context);

            add.UserId = user.Id;

            await _context.Adds.AddAsync(add);
            
            Functions.SendNotificationFromFirebaseCloud(new NotificationData{
                Body ="تم نشر اعلان في المعلقة",
                Desc ="fffffffffff",
                ImageUrl="dddd",
                Title="ddddddddddddd",
                Subject="mdmdmddmd"
            }); 
            _context.SaveChanges();

            return Ok(add);

        }







        [Authorize(Roles = "user")]
        [HttpPost]
        [Route("delete-add")]
        public async Task<ActionResult> DeleteAdd([FromForm] int id)
        {
            User user = await Functions.getCurrentUser(_httpContextAccessor, _context);
            var categoryModelFromRepo = _repository.GetAddById(id);
            if (categoryModelFromRepo == null)
            {
                return NotFound();
            }
            if (categoryModelFromRepo.UserId == user.Id)
            {
                _repository.DeleteAdds(categoryModelFromRepo);
                _repository.SaveChanges();
                return Ok(categoryModelFromRepo);

            }
            else
            {


                return NotFound();
            }





        }




        [HttpPost("{id}")]
        [Route("update-adds")]
        public async Task<ActionResult> UpdateAdd([FromForm] int id, [FromForm] AddsUpdateDto categoryUpdateDto)
        {
            var categoryModelFromRepo = _repository.GetAddById(id);
            if (categoryModelFromRepo == null)
            {
                return NotFound();
            }

            categoryModelFromRepo.Status = categoryUpdateDto.Status;

            await _context.SaveChangesAsync();
            // _mapper.Map(categoryUpdateDto, categoryModelFromRepo);

            // _repository.UpdateAdds(categoryModelFromRepo);

            // _repository.SaveChanges();

            return Ok(categoryModelFromRepo);

        }





        [Route("update-my-add")]
        public ActionResult UpdateMyAdd([FromForm] AddsUpdateDto adds, [FromForm] int id)
        {
            var categoryModelFromRepo = _repository.GetAddById(id);
            if (categoryModelFromRepo == null)
            {
                return NotFound();
            }

            // categoryModelFromRepo = adds  ;

            // await _context.SaveChangesAsync();
            _mapper.Map(adds, categoryModelFromRepo);

            _repository.UpdateAdds(categoryModelFromRepo);

            _repository.SaveChanges();

            return Ok(categoryModelFromRepo);

        }




        [HttpPost]
        [Route("image/dawnload")]

        public string UploadedFile([FromForm] IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }









    }
}