using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;
using AzzanOrder.Data.DTO;

namespace AzzanOrder.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public PromotionsController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Promotions
        [HttpGet("GetPromotionsByEmpId/{id}")]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetPromotionsByEmpId(int id)
        {
          if (_context.Promotions == null)
          {
              return NotFound();
          }
          var promotions = await _context.Promotions.Where(p => p.EmployeeId == id && p.IsActive != false).ToListAsync();
            if (promotions == null)
            {
                return NotFound();
            }
            return promotions;
        }

        // GET: api/Promotions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> GetPromotion(int id)
        {
          if (_context.Promotions == null)
          {
              return NotFound();
          }
            var promotion = await _context.Promotions.FindAsync(id);

            if (promotion == null)
            {
                return NotFound();
            }

            return promotion;
        }

        // PUT: api/Promotions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutPromotion(Promotion promotion)
        {
            _context.Entry(promotion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionExists(promotion.PromotionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(promotion);
        }

        // POST: api/Promotions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Promotion>> PostPromotion(Promotion promotion)
        {
          if (_context.Promotions == null)
          {
              return Problem("Entity set 'OrderingAssistSystemContext.Promotions'  is null.");
          }
            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPromotion", new { id = promotion.PromotionId }, promotion);
        }

        // DELETE: api/Promotions/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            if (_context.Promotions == null)
            {
                return NotFound();
            }
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }

            promotion.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PromotionExists(int id)
        {
            return (_context.Promotions?.Any(e => e.PromotionId == id)).GetValueOrDefault();
        }
        // GET: api/Promotions/GetByDescription/{description}
        [HttpGet("GetByDescription/{description}")]
        public async Task<ActionResult> GetPromotionsByDescription(string description, int? id)
        {
            if (_context.Promotions == null)
            {
                return NotFound();
            }

            var query = _context.Promotions.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(p => p.EmployeeId == id.Value);
            }

            var promotions = await query
                .Where(p => p.Description.Contains(description) && p.IsActive != false)
                .Select(m => new PromotionDTO
                {
                    PromotionId = m.PromotionId,
                    Title = m.Title,
                    Description = m.Description,
                    Image = m.Image,
                    EmployeeId = m.EmployeeId
                })
                .ToListAsync();

            if (promotions == null || !promotions.Any())
            {
                return NotFound();
            }

            if (promotions.Count == 1)
            {
                return Ok(promotions.First());
            }

            return Ok(promotions);
        }

        [HttpPut("ImageAdd/{id}")]
        public async Task<IActionResult> PutImage(int id,[FromBody] string img)
        {
            var member = await _context.Promotions.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            member.Image = img;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
