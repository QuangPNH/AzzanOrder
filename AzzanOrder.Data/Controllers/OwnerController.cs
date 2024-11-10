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
    public class OwnerController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public OwnerController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Owner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners()
        {
            if (_context.Owners == null)
            {
                return NotFound();
            }
            return await _context.Owners.Where(o => o.IsDelete == false).Include(o => o.Bank).ToListAsync();
        }

        // GET: api/Owner/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            if (_context.Owners == null)
            {
                return NotFound();
            }
            var owner = await _context.Owners.Where(o => o.IsDelete == false).Include(o => o.Bank).FirstOrDefaultAsync(o => o.OwnerId == id);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

		[HttpGet("Phone/{phone}")]
		public async Task<ActionResult<Owner>> GetOwnerByPhone(string phone)
		{
			if (_context.Owners == null)
			{
				return NotFound();
			}
			var owner = await _context.Owners.Where(o => o.IsDelete == false).FirstOrDefaultAsync(o => o.Phone.Equals(phone));

			if (owner == null)
			{
				return NotFound();
			}

			return owner;
		}

		// PUT: api/Owner/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("Update")]
        public async Task<IActionResult> PutOwner(Owner owner)
        {
            if (OwnerExists(owner.OwnerId))
            {
                return BadRequest();
            }

            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(owner.OwnerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(owner);
        }

        // POST: api/Owner
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            if (_context.Owners == null)
            {
                return Problem("Entity set 'OrderingAssistSystemContext.Owners'  is null.");
            }
            owner.IsDelete = false;
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new { id = owner.OwnerId }, owner);
        }

        // DELETE: api/Owner/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            if (_context.Owners == null)
            {
                return NotFound();
            }
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            owner.IsDelete = true;
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        private bool OwnerExists(int id)
        {
            return (_context.Owners?.Any(e => e.OwnerId == id)).GetValueOrDefault();
        }
    }
}
