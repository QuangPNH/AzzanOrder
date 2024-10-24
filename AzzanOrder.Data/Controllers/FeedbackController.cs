using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;

namespace AzzanOrder.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public FeedbackController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
          if (_context.Feedbacks == null)
          {
              return NotFound();
          }
            return await _context.Feedbacks.ToListAsync();
        }

        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
          if (_context.Feedbacks == null)
          {
              return NotFound();
          }
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        [HttpGet("ByMemberId/{id}")]
        public async Task<ActionResult<Feedback>> GetFeedbackByMember(int id)
        {
            var feedback = await _context.Feedbacks.Where(x => x.MemberId == id).FirstOrDefaultAsync();

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        // PUT: api/Feedback/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutFeedback(Feedback feedback)
        {
            if (!FeedbackExists(feedback.Feedbackid))
            {
                return NotFound("This feedback not exist");
            }

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(feedback.Feedbackid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Update success");
        }

        // POST: api/Feedback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            if (_context.Feedbacks == null)
            {
                return Problem("Entity set 'OrderingAssistSystemContext.Feedbacks'  is null.");
            }
            var f = new Feedback() { Content = feedback.Content, MemberId = feedback.MemberId };
            _context.Feedbacks.Add(f);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { id = f.Feedbackid }, f);
        }

        // DELETE: api/Feedback/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            if (_context.Feedbacks == null)
            {
                return NotFound();
            }
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        private bool FeedbackExists(int id)
        {
            return (_context.Feedbacks?.Any(e => e.Feedbackid == id)).GetValueOrDefault();
        }
    }
}


//O day nua ne