using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P4.Model;
using P4.Misc;
using Microsoft.EntityFrameworkCore;

namespace P4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly Database database;

        public FeedbackController(Database database)
        {
            this.database = database;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            return await database.Feedbacks.ToListAsync();
        }
        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            var feedback = await database.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return feedback;
        }
        // POST: api/Feedback
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(int custId, Feedback feedback)
        {
            feedback.CustId = custId;
            database.Feedbacks.Add(feedback);
            await database.SaveChangesAsync();
            return CreatedAtAction("GetFeedback", new { id = feedback.FeedId }, feedback);
        }
        // DELETE: api/Feedback/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await database.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            database.Feedbacks.Remove(feedback);
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
