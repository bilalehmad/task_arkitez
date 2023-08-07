using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskProject.DAL;
using TaskProject.DBContext;
using TaskProject.Models;
using TaskProject.Models.DTO;

namespace TaskProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DBContextIdentity dBContext;
        private ICategoryRepository categoryRepository;

        public CategoryController(DBContextIdentity dBContext)
        {
            this.dBContext = dBContext;
            this.categoryRepository = new CategoryRepository(dBContext);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try { 

                IEnumerable<CategoriesDTO> cat = categoryRepository.GetCategories().Select(s => new CategoriesDTO
                {
                    CatID = s.CatID,
                    Name = s.Name,
                    Discription = s.Discription
                });
                return StatusCode(StatusCodes.Status200OK,cat);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error on Getting Data");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try {
                if (id == 0)
                    return BadRequest("Category ID Not Define");

                Categories getData = categoryRepository.GetCategoryByID(id);

                if (getData == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }

                CategoriesDTO setDto = new CategoriesDTO { CatID = getData.CatID, Name = getData.Name, Discription = getData.Discription };

                return Ok(setDto);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error on Getting Data");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoriesDTO categoriesDTO)
        {
            try
            {
                Categories getData = new Categories
                {
                    CatID = categoriesDTO.CatID,
                    Name = categoriesDTO.Name,
                    Discription = categoriesDTO.Discription
                };

                categoryRepository.InsertCategory(getData);
                categoryRepository.Save();

                CategoriesDTO setDto = new CategoriesDTO { CatID = getData.CatID, Name = getData.Name, Discription = getData.Discription };

                return CreatedAtAction(nameof(Get), new { id = setDto.CatID }, setDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error on Updating data");
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] CategoriesDTO categoriesDTO)
        {

            try
            {
                if (id == 0)
                    return BadRequest("Category ID Not Define");

                Categories getData = new Categories
                {
                    CatID = categoriesDTO.CatID,
                    Name = categoriesDTO.Name,
                    Discription = categoriesDTO.Discription
                };

                categoryRepository.UpdateCategory(getData);
                categoryRepository.Save();

                return CreatedAtAction(nameof(Get), new { id = id }, getData);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error on Updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {

            try
            {
                if (id == 0)
                    return BadRequest("Category ID Not Define");

                categoryRepository.DeleteCategory(id);
                categoryRepository.Save();

                return StatusCode(StatusCodes.Status200OK,
                    "Data is Deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error on Updating data");
            }
        }

    }
}
