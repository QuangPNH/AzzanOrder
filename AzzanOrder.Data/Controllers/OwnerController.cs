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

		// GET: api/Owner/5
		[HttpGet("Manager/{id}")]
		public async Task<ActionResult<Owner>> GetOwnerByManagerId(int id)
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

        [HttpGet("Gmail/{gmail}")]
        public async Task<ActionResult<Owner>> GetOwnerByGmail(string gmail)
        {
            if (_context.Owners == null)
            {
                return NotFound();
            }
            var owner = await _context.Owners.Where(o => o.IsDelete == false).FirstOrDefaultAsync(o => o.Gmail.ToLower().Equals(gmail.ToLower()));

            if (owner == null)
            {
                return NotFound();
            }
            return owner;
        }

        [HttpPatch("UpdateSubscriptionDatesByPhone/{phone}")]
		public async Task<IActionResult> UpdateSubscriptionDatesByPhone(string phone, DateTime subscriptionStartDate, DateTime subscriptionEndDate)
		{
			if (_context.Owners == null)
			{
				return NotFound();
			}

			var owner = await _context.Owners.FirstOrDefaultAsync(o => o.Phone == phone && o.IsDelete == false);
			if (owner == null)
			{
				return NotFound();
			}

			owner.SubscribeEndDate = owner.SubscriptionStartDate.Add(subscriptionEndDate - subscriptionStartDate);
			owner.SubscriptionStartDate = subscriptionStartDate;
			

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


		// PUT: api/Owner/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("Update")]
		public async Task<IActionResult> PutOwner(Owner owner)
		{
			var data = _context.Owners.Find(owner.OwnerId);
			if(data == null)
			{
				return NotFound();
			}

			_context.Entry(data).CurrentValues.SetValues(owner);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return BadRequest();
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

			if (_context.Owners.Any(o => o.Phone == owner.Phone))
			{
				return BadRequest("Phone number already exists");
			}

			_context.Owners.Add(owner);
			await _context.SaveChangesAsync();
           
			return Ok(owner);
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
