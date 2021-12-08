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
    public class BasketController : ControllerBase
    {
        private readonly Database database;

        public BasketController(Database database)
        {
            this.database = database;
        }

        // GET: api/Basket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Basket>>> GetBaskets()
        {
            using (database)
            {
                List<Basket> baskets = await database.Baskets.FromSqlRaw("select * from Baskets").ToListAsync();
                foreach (var basket in baskets)
                {
                    var customer = await database.Customers.FindAsync(basket.CustId);
                    List<BasketWithProduct> products = await database.BasketWithProducts.FromSqlRaw("select * from BasketWithProducts").Where(bwp => bwp.BasketId == basket.BasketId).ToListAsync();
                    foreach (var p in products)
                    {
                        var product = await database.Products.FindAsync(p.ProdId);
                    }
                }
                // return result;
                return Ok(baskets);
            }
        }
        // GET: api/Basket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetBasket(int id)
        {
            using (database)
            {
                var basket = await database.Baskets.FindAsync(id);
                if (basket == null)
                {
                    return NotFound();
                }
                List<BasketWithProduct> products = await database.BasketWithProducts.FromSqlRaw("select * from BasketWithProducts").Where(bwp => bwp.BasketId == basket.BasketId).ToListAsync();
                foreach (var p in products)
                {
                    var product = await database.Products.FindAsync(p.ProdId);
                }
                return Ok(basket);
            }
        }
        // PATCH: api/Basket/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBasket(int id, int prodId, string basketStatus, [FromBody] JsonPatchDocument<BasketWithProduct> patchDocument)
        {
            var basket = await database.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(basketStatus))
            {
                basket.BasketStatus = basketStatus;
            }
            if (prodId != 0)
            {
                var product = await database.BasketWithProducts.FromSqlRaw("select * from BasketWithProducts").Where(bwp => bwp.BasketId == basket.BasketId && bwp.ProdId == prodId).FirstOrDefaultAsync();
                if (product == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(product, ModelState);
            }
            try
            {
                await database.SaveChangesAsync();
                return Ok("Patch is successful");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!database.Baskets.Any(b => b.BasketId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        // POST: api/Basket
        [HttpPost]
        public async Task<ActionResult<Basket>> PostBasket(int custId, string basketStatus, List<BasketWithProduct> basketWithProducts)
        {
            var basket = new Basket { BasketStatus = basketStatus, CustId = custId };
            database.Baskets.Add(basket);
            await database.SaveChangesAsync();
            foreach (var basketWithProduct in basketWithProducts)
            {
                basketWithProduct.BasketId = basket.BasketId;
            }
            database.BasketWithProducts.AddRange(basketWithProducts);
            await database.SaveChangesAsync();
            return CreatedAtAction("GetBasket", new { id = basket.BasketId }, basket);
        }
        // DELETE: api/Basket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(int id)
        {
            var basketModel = await database.Baskets.FindAsync(id);
            if (basketModel == null)
            {
                return NotFound();
            }
            database.BasketWithProducts.RemoveRange(database.BasketWithProducts.Where(bwp => bwp.BasketId == basketModel.BasketId));
            database.Baskets.Remove(basketModel);
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
