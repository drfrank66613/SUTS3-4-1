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
    public class SaleController : ControllerBase
    {
        private readonly Database database;

        public SaleController(Database database)
        {
            this.database = database;
        }

        // GET: api/Sale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            using (database)
            {
                List<Sale> sales = await database.Sales.FromSqlRaw("select * from Sales").ToListAsync();
                foreach (var sale in sales)
                {
                    var cust = await database.Customers.FindAsync(sale.CustId);
                    List<SaleWithProduct> products = await database.SaleWithProducts.FromSqlRaw("select * from SaleWithProducts").Where(swp => swp.SaleId == sale.SaleId).ToListAsync();
                    foreach (var p in products)
                    {
                        var product = await database.Sales.FindAsync(p.ProdId);
                    }
                }
                return Ok(sales);
            }
        }
        // GET: api/Sale/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            using (database)
            {
                var sale = await database.Sales.FindAsync(id);
                if (sale == null)
                {
                    return NotFound();
                }
                List<SaleWithProduct> products = await database.SaleWithProducts.FromSqlRaw("select * from SaleWithProducts").Where(swp => swp.SaleId == sale.SaleId).ToListAsync();
                foreach (var p in products)
                {
                    var product = await database.Sales.FindAsync(p.ProdId);
                }
                return sale;
            }
        }
        // PATCH: api/Sale/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSale(int id, int prodId, String saleStatus, [FromBody] JsonPatchDocument<Sale> patchDocument)
        {
            var sale = await database.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(saleStatus))
            {
                sale.SaleStatus = saleStatus;
            }
            if (patchDocument != null)
            {
                patchDocument.ApplyTo(sale, ModelState);
                await database.SaveChangesAsync();
                return Ok("Patch is successful");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // POST: api/Sale
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(int custId, string saleStatus, List<SaleWithProduct> saleWithProducts)
        {
            var sale = new Sale { SaleStatus = saleStatus, CustId = custId };
            database.Sales.Add(sale);
            await database.SaveChangesAsync();
            foreach (var saleWithProduct in saleWithProducts)
            {
                saleWithProduct.SaleId = sale.SaleId;
            }
            database.SaleWithProducts.AddRange(saleWithProducts);
            await database.SaveChangesAsync();
            return CreatedAtAction("GetSale", new { id = sale.SaleId }, sale);
        }
        // DELETE: api/Sale/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await database.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            database.SaleWithProducts.RemoveRange(database.SaleWithProducts.Where(swp => swp.SaleId == sale.SaleId));
            database.Sales.Remove(sale);
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
