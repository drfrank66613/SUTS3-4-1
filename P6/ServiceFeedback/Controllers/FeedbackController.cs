using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceFeedback.Misc;
using ServiceFeedback.Model;

namespace ServiceFeedback.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackContext context;

        public FeedbackController(FeedbackContext context)
        {
            this.context = context;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            return await context.Feedbacks.ToListAsync();
        }

        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            var feedback = await context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return feedback;
        }

        // POST: api/Feedback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            context.Feedbacks.Add(feedback);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetFeedback", new { id = feedback.FeedId }, feedback);
        }

        // DELETE: api/Feedback/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            context.Feedbacks.Remove(feedback);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
