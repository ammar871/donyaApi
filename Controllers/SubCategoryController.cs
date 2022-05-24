using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Commander.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {

        private readonly CommanderContext _context;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly ISubCategoryRepo _repository;
        private IMapper _mapper;



        public SubCategoryController(CommanderContext context, IWebHostEnvironment hostingEnvironment, ISubCategoryRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }



        [HttpGet]
        [Route("get-sub-categories")]
        public async Task<ActionResult> GetAll()
        {
            var data = await _context.SubCategories.ToListAsync();

            List<ResponseSubCategory> responseSubCategories = new List<ResponseSubCategory>();

            foreach (var item in data)
            {

                int conter = _context.Adds.Where(p => p.CategoryId == item.Id && p.Status == 1).Count();
                responseSubCategories.Add(
                    new ResponseSubCategory
                    {
                        SubCategory = item,
                        Conter = conter
                    }
                );
            }
            return Ok(responseSubCategories);






        }


        [HttpPost]
        [Route("add-sub-category")]
        public ActionResult<SubCategory> CreateCategory([FromForm] SubCategoryCreateDto subCategoryCreateDto)
        {

            SubCategory newModel = new SubCategory
            {
                CategoryId = subCategoryCreateDto.CategoryId,
                NameArbice = subCategoryCreateDto.NameArbice,
                NameEnglish = subCategoryCreateDto.NameEnglish,
                NameFrance = subCategoryCreateDto.NameFrance,
                date = DateTime.Now,
                Image = subCategoryCreateDto.Image

            };


            var coomansModel = _mapper.Map<SubCategory>(newModel);
            _repository.CreateSubCategory(coomansModel);
            _repository.SaveChanges();

            return Ok(coomansModel);





        }


        [HttpPost]
        [Route("delete-sub-category")]
        public ActionResult DeleteCategory([FromForm] int id)
        {

            var categoryModelFromRepo = _repository.GetSubCategoryById(id);
            if (categoryModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteSubCategory(categoryModelFromRepo);
            _repository.SaveChanges();
            return Ok(categoryModelFromRepo.NameArbice + "Deleted");



        }



        [HttpGet]
        [Route("sub-categorybyid")]
        public ActionResult<SubCategory> GetSubCategoryByID([FromForm] int id)
        {
            var commandItem = _repository.GetSubCategoryById(id);
            if (commandItem != null)
            {

                return Ok(commandItem);
            }
            else
            {

                return NotFound();
            }



        }



        [HttpGet]
        [Route("subCategories-By-CategId")]
        public async Task<ActionResult> GetSubCategoryByCategoryID([FromForm] int id, [FromForm] string cuntry)
        {
            var data = await _context.SubCategories.Where(p => p.CategoryId == id).ToListAsync();

            List<ResponseSubCategory> responseSubCategories = new List<ResponseSubCategory>();

            if (cuntry == null)
            {
                foreach (var item in data)
                {

                    int conter = _context.Adds.Where(p => p.CategoryId == item.Id && p.Status == 1).Count();
                    responseSubCategories.Add(
                     new ResponseSubCategory
                     {
                         SubCategory = item,
                         Conter =1000+ conter
                     }
                 );



                }

            }
            else
            {
                foreach (var item in data)
                {

                    int conter = _context.Adds.Where(p => p.CategoryId == item.Id && p.Status == 1 && p.Country == cuntry).Count();
                    responseSubCategories.Add(
                     new ResponseSubCategory
                     {
                         SubCategory = item,
                         Conter =1000 + conter
                     }
                 );



                }
            }



            return Ok(responseSubCategories);




        }






        [HttpPost("{id}")]
        [Route("update-sub-category")]
        public ActionResult UpdateSubCategory([FromForm] int id, [FromForm] SubCategoryUpdateDto categoryUpdateDto)
        {
            var categoryModelFromRepo = _repository.GetSubCategoryById(id);
            if (categoryModelFromRepo == null)
            {
                return NotFound();
            }


            _mapper.Map(categoryUpdateDto, categoryModelFromRepo);

            _repository.UpdateSubCategory(categoryModelFromRepo);

            _repository.SaveChanges();

            return NoContent();

        }









    }
}