using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{


    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {


        private IWebHostEnvironment _hostingEnvironment;
        private readonly ICategoryRepo _repository;
        private IMapper _mapper;



        public CategoryController(IWebHostEnvironment hostingEnvironment, ICategoryRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }


        
        [HttpGet]
        [Route("get-categories")]
        public ActionResult<IEnumerable<CategoryReadDto>> GetAll()
        {
            var commandItems = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CategoryReadDto>>(commandItems));
        }


        [HttpPost]
        [Route("add-category")]
        public ActionResult<Category> CreateCategory([FromForm] CreateCategory commandCreateDto)
        {


            CreateCategory newModel = new CreateCategory
            {
                NameArbice = commandCreateDto.NameArbice,
                NameEnglish = commandCreateDto.NameEnglish,
                NameFrance = commandCreateDto.NameFrance,
                Image = commandCreateDto.Image

            };


            var coomansModel = _mapper.Map<Category>(newModel);
            _repository.CreateCategory(coomansModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CategoryReadDto>(coomansModel);
            // return Ok(commandReadDto);



            return Ok(coomansModel);








        }


        [HttpPost]
        [Route("delete-category")]
        public ActionResult DeleteCategory([FromForm] int id)
        {

            var categoryModelFromRepo = _repository.GetCategoryById(id);
            if (categoryModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCategory(categoryModelFromRepo);
            _repository.SaveChanges();
            return Ok(categoryModelFromRepo.NameArbice + "Deleted");



        }



        [HttpPost("{id}")]
        [Route("update-category")]
        public ActionResult UpdateCategory([FromForm] int id, [FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            var categoryModelFromRepo = _repository.GetCategoryById(id);
            if (categoryModelFromRepo == null)
            {
                return NotFound();
            }


            _mapper.Map(categoryUpdateDto, categoryModelFromRepo);

            _repository.UpdateCategory(categoryModelFromRepo);

            _repository.SaveChanges();

            return NoContent();

        }



    }
}