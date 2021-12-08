using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P4.Model;
using P4.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace P4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly Database database;

        public ProductController(Database database)
        {
            this.database = database;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            using (database)
            {
                return await database.Products.ToListAsync();
            }
        }
        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await database.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProdId)
            {
                return BadRequest();
            }
            database.Entry(product).State = EntityState.Modified;
            try
            {
                await database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!database.Products.Any(e => e.ProdId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        // PATCH: api/Product/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(int id, [FromBody] JsonPatchDocument<Product> value)
        {
            var product = database.Products.FirstOrDefault(p => p.ProdId == id);
            if (id != product.ProdId)
            {
                return BadRequest();
            }
            if (value != null)
            {
                value.ApplyTo(product, ModelState);
                await database.SaveChangesAsync();
                return Ok("Patch is successful");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            database.Products.Add(product);
            await database.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.ProdId }, product);
        }
        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await database.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            database.Products.Remove(product);
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
