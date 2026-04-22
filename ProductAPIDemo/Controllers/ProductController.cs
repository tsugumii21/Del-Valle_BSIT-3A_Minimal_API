using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPIDemo.Data;
using ProductAPIDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductAPIDemo.Controllers
{
    [Route("api/[controller]")] // api/Product
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public ProductController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _dbContext.Products.ToListAsync();
            return Ok(products);
        }

        //Post: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return Ok(product);
        }

        //Put: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProducts(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _dbContext.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Product updated Successfully" });
        }

        //Delete: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _dbContext.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            _dbContext.Products.Remove(existingProduct);

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Product Deleted Successfully" });
        }
    }
}
