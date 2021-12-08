using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceProduct.Misc;
using ServiceProduct.Model;

namespace ServiceProduct.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext context;

        public ProductController(ProductContext context)
        {
            this.context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProdId)
            {
                return BadRequest();
            }
            context.Entry(product).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Products.Any(c => c.ProdId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        // PATCH: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProductModel(int id, [FromBody] JsonPatchDocument<Product> patchDocument)
        {
            var product = context.Products.FirstOrDefault(m => m.ProdId == id);
            if (id != product.ProdId)
            {
                return BadRequest();
            }
            if (patchDocument != null)
            {
                patchDocument.ApplyTo(product, ModelState);
                await context.SaveChangesAsync();
                return Ok("Patch is successful");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.ProdId }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
