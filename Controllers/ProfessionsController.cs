using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Helpers;
using Commander.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace Commander.Controllers
{

    [ApiController]
    public class ProfessionsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private UserManager<User> userManager;
        private readonly IConfiguration _config;
        private readonly CommanderContext _context;
        public readonly IWebHostEnvironment _webHostEnvironment;



        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfessionsController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, CommanderContext context
                     )
        {
            this._roleManager = roleManager;
            this._mapper = mapper;
            this.userManager = userManager;
            this._config = config;
            this._webHostEnvironment = webHostEnvironment;
            this._context = context;
            _httpContextAccessor = httpContextAccessor;
        }



        //Category
        [HttpPost]
        [Route("add-profession")]
        public async Task<ActionResult> CreateProfession([FromForm] CategoryProfission category)
        {



            await _context.CategoryProfession.AddAsync(category);

            await _context.SaveChangesAsync();



            return Ok(category);


        }


        [HttpGet]
        [Route("get-professions-category")]
        public async Task<ActionResult> GetCategoryProfession()
        {
            var data = await _context.CategoryProfession.ToListAsync();
            return Ok(data);
        }

        [HttpPost]
        [Route("delete-profession")]
        public async Task<ActionResult> DeleteProfession([FromForm] int id)
        {


            CategoryProfission post = _context.CategoryProfession.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {

                return NotFound();
            }


            _context.CategoryProfession.Remove(post);
            await _context.SaveChangesAsync();
            return Ok(post);



        }


        [HttpPost("{id}")]
        [Route("update-profession")]
        public ActionResult UpdateCategory([FromForm] int id, [FromForm] CategoryProfessionUpdateDto categoryUpdateDto)
        {
            CategoryProfission categoryProfission = _context.CategoryProfession.FirstOrDefault(p => p.Id == id);
            if (categoryProfission == null)
            {
                return NotFound();
            }


            _mapper.Map(categoryUpdateDto, categoryProfission);

            _context.SaveChanges();

            return NoContent();

        }


        //adds ======================================================================================================


        [HttpPost]
        [Route("add-profession-add")]
        public async Task<ActionResult> CreateProfessionAdd([FromForm] Profession profession)
        {



            await _context.AddsProfession.AddAsync(profession);

            await _context.SaveChangesAsync();



            return Ok(profession);


        }



        [HttpGet]
        [Route("get-professions-adds")]
        public async Task<ActionResult> GetAddsProfession()
        {
            var data = await _context.AddsProfession.ToListAsync();
            return Ok(data);
        }



        [HttpGet]
        [Route("professions-adds-by-id")]
        public async Task<ActionResult> GetAddsProfessionById([FromForm] int id)
        {
            var data = await _context.AddsProfession.Where(p => p.CategoryId == id).ToListAsync();
            return Ok(data);
        }


        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("professions-my-adds")]
        public async Task<ActionResult> GetAddsMyProfession([FromForm] int id)
        {
            User user = await Functions.getCurrentUser(_httpContextAccessor, _context);
            var data = await _context.AddsProfession.Where(p => p.UserId == user.Id).ToListAsync();
            return Ok(data);
        }



        [HttpPost]
        [Route("delete-profession-add")]
        public async Task<ActionResult> DeleteProfessionAdd([FromForm] int id)
        {


            Profession post = _context.AddsProfession.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {

                return NotFound();
            }


            _context.AddsProfession.Remove(post);
            await _context.SaveChangesAsync();
            return Ok(post);



        }



        [HttpPost("{id}")]
        [Route("update-profession-add")]
        public ActionResult UpdateAddProfession([FromForm] int id, [FromForm] ProfessionUpdate categoryUpdateDto)
        {
            Profession categoryProfission = _context.AddsProfession.FirstOrDefault(p => p.Id == id);
            if (categoryProfission == null)
            {
                return NotFound();
            }


            _mapper.Map(categoryUpdateDto, categoryProfission);

            _context.SaveChanges();

            return NoContent();

        }



        //create Account Profession
        [HttpPost]
        [Route("create-professional-account")]
        public async Task<ActionResult> CreateProfessional([FromForm] professional profession)
        {


            professional professional = _context.Professioners.FirstOrDefault(p => p.Phone == profession.Phone);

            if (professional == null)
            {

                await _context.Professioners.AddAsync(profession);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    StatusCode = true,
                    message = "تم التسجيل",
                    professional = profession
                });
            }
            else
            {

                return Ok(
                    new
                    {
                        StatusCode = false,
                        message = "الأكونت موجود",
                        professional = profession
                    }

                    );
            }

        }

        [HttpPost]
        [Route("professional-login")]
        public ActionResult LoginProfessional([FromForm] string Phone, [FromForm] string Password)
        {


            professional professional = _context.Professioners.FirstOrDefault(p => p.Phone == Phone && p.Password == Password);

            if (professional == null)
            {
                return NotFound();

            }

            return Ok(professional);

        }



        [HttpPost]
        [Route("delete-professional-account")]
        public async Task<ActionResult> DeleteProfessionAcount([FromForm] int id)
        {


            professional post = _context.Professioners.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {

                return NotFound();
            }


            _context.Professioners.Remove(post);
            await _context.SaveChangesAsync();
            return Ok(post);



        }


        [HttpGet]
        [Route("get-professionals-account")]
        public async Task<ActionResult> GetAddsProfessionAccount()
        {
            var data = await _context.Professioners.ToListAsync();
            return Ok(data);
        }





        [HttpPost("{id}")]
        [Route("update-professional-account")]
        public ActionResult UpdateAddProfessionAccount([FromForm] int id, [FromForm] ProfessionalUpdate categoryUpdateDto)
        {
            professional categoryProfission = _context.Professioners.FirstOrDefault(p => p.Id == id);
            if (categoryProfission == null)
            {
                return NotFound();
            }


            _mapper.Map(categoryUpdateDto, categoryProfission);

            _context.SaveChanges();

            return NoContent();

        }


        [HttpPost()]
        [Route("forget-pass")]
        public ActionResult ForgetPass([FromForm] string phone)
        {
            professional categoryProfission = _context.Professioners.FirstOrDefault(p => p.Phone == phone);
            if (categoryProfission == null)
            {
                return NotFound();
            }




            return Ok(categoryProfission);

        }


    }




}

