using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceSale.Misc;
using ServiceSale.Model;

namespace ServiceSale.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly SaleContext context;

        public SaleController(SaleContext context)
        {
            this.context = context;
        }

        // GET: api/Sale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await context.Sales.ToListAsync();
        }

        // GET: api/Sale/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            var sale = await context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return sale;
        }

        // PATCH: api/Sale/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSaleModel(int id, string saleStatus, [FromBody] JsonPatchDocument<Sale> patchDocument)
        {
            var sale = await context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(saleStatus))
            {
                sale.SaleStatus = saleStatus;
            }
            patchDocument.ApplyTo(sale, ModelState);
            try
            {
                await context.SaveChangesAsync();
                return Ok("Patch is successful");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Sales.Any(s => s.SaleId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Sale
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
            context.Sales.Add(sale);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetSale", new { id = sale.SaleId }, sale);
        }

        // DELETE: api/Sale/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            context.Sales.Remove(sale);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
