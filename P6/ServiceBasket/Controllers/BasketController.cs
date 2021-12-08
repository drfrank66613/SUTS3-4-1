using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceBasket.Misc;
using ServiceBasket.Model;

namespace ServiceBasket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly BasketContext context;

        public BasketController(BasketContext context)
        {
            this.context = context;
        }

        // GET: api/Basket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Basket>>> GetBaskets()
        {
            return await context.Baskets.ToListAsync();
        }

        // GET: api/Basket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetBasket(int id)
        {
            var basket = await context.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            return basket;
        }

        // PATCH: api/Basket/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBasketModel(int id, string basketStatus, [FromBody] JsonPatchDocument<Basket> patchDocument)
        {

            var basket = await context.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(basketStatus))
            {
                basket.BasketStatus = basketStatus;
            }
            patchDocument.ApplyTo(basket, ModelState);
            try
            {
                await context.SaveChangesAsync();
                return Ok("Patch is successful");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Baskets.Any(b => b.BasketId == id))
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
        public async Task<ActionResult<Basket>> PostBasket(Basket basket)
        {
            context.Baskets.Add(basket);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetBasket", new { id = basket.BasketId }, basket);
        }

        // DELETE: api/Basket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(int id)
        {
            var basket = await context.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            context.Baskets.Remove(basket);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
