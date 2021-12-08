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
    public class CustomerController : ControllerBase
    {
        private readonly Database database;

        public CustomerController(Database database)
        {
            this.database = database;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await database.Customers.ToListAsync();
        }
        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await database.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }
        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustId)
            {
                return BadRequest();
            }
            database.Entry(customer).State = EntityState.Modified;
            try
            {
                await database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!database.Customers.Any(c => c.CustId == id))
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
        // PATCH: api/Customer/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProductModel(int id, [FromBody] JsonPatchDocument<Customer> patchDocument)
        {
            var customer = database.Customers.FirstOrDefault(m => m.CustId == id);
            if (id != customer.CustId)
            {
                return BadRequest();
            }
            if (patchDocument != null)
            {
                patchDocument.ApplyTo(customer, ModelState);
                await database.SaveChangesAsync();
                return Ok("Patch is successful");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            database.Customers.Add(customer);
            await database.SaveChangesAsync();
            return CreatedAtAction("GetCustomer", new { id = customer.CustId }, customer);
        }
        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await database.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            database.Customers.Remove(customer);
            await database.SaveChangesAsync();
            return NoContent();
        }
    }
}
