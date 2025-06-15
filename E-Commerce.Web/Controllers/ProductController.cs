using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Automatic Model Validation
    public class ProductController : ControllerBase
    {
        // GET: api/Product/id
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return new Product() { Id = id };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return new List<Product>
            {
                new Product { Id = 1 },
                new Product { Id = 2 },
                new Product { Id = 3 }
            };
        }

        [HttpPost]
        public ActionResult<Product> AddProduct()
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult<Product> UpdateProduct()
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult<Product> DeleteProduct()
        {
            return Ok();
        }
    }
}
