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
    public class AboutsController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public AboutsController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Abouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<About>>> GetAbouts(int ?ownerId)
        {
          if (_context.Abouts == null)
          {
              return NotFound();
          }
            var a = ownerId.HasValue ? await _context.Abouts.Where(a => a.OwnerId == ownerId).ToListAsync() : await _context.Abouts.ToListAsync();
            return a;
        }

        // GET: api/Abouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<About>> GetAbout(int id)
        {
          if (_context.Abouts == null)
          {
              return NotFound();
          }
            var about = await _context.Abouts.FindAsync(id);

            if (about == null)
            {
                return NotFound();
            }

            return about;
        }

        // PUT: api/Abouts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutAbout(About about)
        {
            if (!AboutExists(about.AboutId))
            {
                return NotFound("This is not exist");
            }

            _context.Entry(about).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutExists(about.AboutId))
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

        // POST: api/Abouts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<About>> PostAbout(About about)
        {
          if (_context.Abouts == null)
          {
              return Problem("Entity set 'OrderingAssistSystemContext.Abouts'  is null.");
          }
            var ab = new About() { Title = about.Title, Content = about.Content, OwnerId = about.OwnerId };
            _context.Abouts.Add(ab);
            await _context.SaveChangesAsync();
            
            return Ok(ab);
        }

        // DELETE: api/Abouts/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            if (_context.Abouts == null)
            {
                return NotFound();
            }
            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        private bool AboutExists(int id)
        {
            return (_context.Abouts?.Any(e => e.AboutId == id)).GetValueOrDefault();
        }
    }
}
