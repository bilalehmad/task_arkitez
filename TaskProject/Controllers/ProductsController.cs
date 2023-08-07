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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DBContextIdentity dBContext;
        private IProductRepository productRepository;

        public ProductsController(DBContextIdentity dBContext)
        {
            this.dBContext = dBContext;
            this.productRepository = new ProductRepository(dBContext);
        }

        [HttpGet]
        public IEnumerable<ProductsDTO> GetAll()
        {
            IEnumerable<ProductsDTO> pro = productRepository.GetProducts().Select(s => new ProductsDTO
            {
                ProdID = s.ProdID,
                Name = s.Name,
                Discription = s.Discription,
                Category = s.Categories.Name
            });
            return pro;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Product ID Not Define");

                Products getData = productRepository.GetProductByID(id);

                if (getData == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                ProductsDTO setDto = new ProductsDTO { ProdID = getData.ProdID, Name = getData.Name, Discription = getData.Discription, Category = getData.Categories.Name };

                return Ok(setDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error on Getting Data");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductsDTO productsDTO)
        {
            try
            {
                Products getData = new Products
                {
                    Name = productsDTO.Name,
                    Discription = productsDTO.Discription,
                    CatID = productsDTO.CatID
                };

                productRepository.InsertProduct(getData);
                productRepository.Save();

                ProductsDTO setDto = new ProductsDTO { ProdID = getData.ProdID, Name = getData.Name, Discription = getData.Discription };

                return CreatedAtAction(nameof(Get), new { id = setDto.ProdID }, setDto);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    err.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductsDTO productsDTO)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Category ID Not Define");

                Products getData = new Products
                {
                    ProdID = productsDTO.ProdID,
                    Name = productsDTO.Name,
                    Discription = productsDTO.Discription,
                    CatID = productsDTO.CatID
                };

                productRepository.UpdateProduct(getData);
                productRepository.Save();

                return CreatedAtAction(nameof(Get), new { id = id }, getData);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error on Updating data");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Product ID Not Define");

                productRepository.DeleteProduct(id);
                productRepository.Save();

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
