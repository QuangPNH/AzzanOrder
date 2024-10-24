using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;
using System.Collections;

namespace AzzanOrder.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public ItemCategoryController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/ItemCategory
        [HttpGet]
		public async Task<ActionResult<IEnumerable<ItemCategory>>> GetItemCategories()
		{
			if (_context.ItemCategories == null)
			{
				return NotFound();
			}

			var itemCategories = await _context.ItemCategories
				.Where(ic => !ic.Description.Contains("TOPPING"))
				.ToListAsync();

			return itemCategories;
		}

        // GET: api/ItemCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable>> GetItemCategory(int id)
        {
          if (_context.ItemCategories == null)
          {
              return NotFound();
          }
            if(id == 0)
            {
                return await _context.ItemCategories.ToListAsync();
            }
            var itemCategory = await _context.ItemCategories.Where(ic=>ic.ItemCategoryId == id).ToListAsync();

            if (itemCategory == null)
            {
                return NotFound();
            }

            return itemCategory;
        }

        // PUT: api/ItemCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCategory(int id, ItemCategory itemCategory)
        {
            if (id != itemCategory.ItemCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(itemCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItemCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemCategory>> PostItemCategory(ItemCategory itemCategory)
        {
          if (_context.ItemCategories == null)
          {
              return Problem("Entity set 'OrderingAssistSystemContext.ItemCategories'  is null.");
          }
            _context.ItemCategories.Add(itemCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCategory", new { id = itemCategory.ItemCategoryId }, itemCategory);
        }

        // DELETE: api/ItemCategory/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteItemCategory(int id)
        {
            if (_context.ItemCategories == null)
            {
                return NotFound();
            }
            var itemCategory = await _context.ItemCategories.FindAsync(id);
            if (itemCategory == null)
            {
                return NotFound();
            }
            var menuCategory = await _context.MenuCategories.Where(mc => mc.ItemCategoryId == itemCategory.ItemCategoryId).ToListAsync();
            if (menuCategory.Count() > 0) {
                return Conflict("Had exist data use this category");
            }

            _context.ItemCategories.Remove(itemCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemCategoryExists(int id)
        {
            return (_context.ItemCategories?.Any(e => e.ItemCategoryId == id)).GetValueOrDefault();
        }
    }
}
