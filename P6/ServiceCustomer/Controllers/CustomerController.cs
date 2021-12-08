using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCustomer.Misc;
using ServiceCustomer.Model;

namespace ServiceCustomer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext context;

        public CustomerController(CustomerContext context)
        {
            this.context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await context.Customers.ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }
        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustId)
            {
                return BadRequest();
            }
            context.Entry(customer).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Customers.Any(c => c.CustId == id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProductModel(int id, [FromBody] JsonPatchDocument<Customer> patchDocument)
        {
            var customer = context.Customers.FirstOrDefault(m => m.CustId == id);
            if (id != customer.CustId)
            {
                return BadRequest();
            }
            if (patchDocument != null)
            {
                patchDocument.ApplyTo(customer, ModelState);
                await context.SaveChangesAsync();
                return Ok("Patch is successful");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetCustomer", new { id = customer.CustId }, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
