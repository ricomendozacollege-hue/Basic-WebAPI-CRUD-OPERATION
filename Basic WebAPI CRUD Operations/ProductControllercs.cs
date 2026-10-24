using Microsoft.AspNetCore.Mvc;

namespace Basic_WebAPI_CRUD_Operations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        
        private static List<Product> products = new List<Product>
        {
            new Product { ProductId = 1, Code = "7811", Name = "squishy", Description = "relieve stress", Price = 100.10m }
        };

        
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }
            return Ok(product);
        }

        
        [HttpPost]
        public IActionResult Create([FromBody] Product newProduct)
        {
            newProduct.ProductId = products.Count > 0 ? products.Max(p => p.ProductId) + 1 : 1;
            products.Add(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.ProductId }, newProduct);
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product updatedProduct)
        {
            var existingProduct = products.FirstOrDefault(p => p.ProductId == id);
            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            existingProduct.Code = updatedProduct.Code;
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;   

            return Ok(existingProduct);
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            products.Remove(product);
            return NoContent();
        }
    }
}