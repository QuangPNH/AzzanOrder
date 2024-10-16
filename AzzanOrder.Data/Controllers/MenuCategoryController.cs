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
    public class MenuCategoryController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public MenuCategoryController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/MenuCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuCategory>>> GetMenuCategories()
        {
          if (_context.MenuCategories == null)
          {
              return NotFound();
          }
            return await _context.MenuCategories.ToListAsync();
        }

        // GET: api/MenuCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuCategory>> GetMenuCategory(int id)
        {
          if (_context.MenuCategories == null)
          {
              return NotFound();
          }
            var menuCategory = await _context.MenuCategories.FindAsync(id);
            if (menuCategory == null)
            {
                return NotFound();
            }
            return menuCategory;
        }

        // PUT: api/MenuCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuCategory(int id, MenuCategory menuCategory)
        {
            if (id != menuCategory.MenuItemId)
            {
                return BadRequest();
            }

            _context.Entry(menuCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuCategoryExists(id))
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

        // POST: api/MenuCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuCategory>> PostMenuCategory(MenuCategory menuCategory)
        {
          if (_context.MenuCategories == null)
          {
              return Problem("Entity set 'OrderingAssistSystemContext.MenuCategories'  is null.");
          }
            _context.MenuCategories.Add(menuCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MenuCategoryExists(menuCategory.MenuItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMenuCategory", new { id = menuCategory.MenuItemId }, menuCategory);
        }

        // DELETE: api/MenuCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuCategory(int id)
        {
            if (_context.MenuCategories == null)
            {
                return NotFound();
            }
            var menuCategory = await _context.MenuCategories.FindAsync(id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            _context.MenuCategories.Remove(menuCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuCategoryExists(int id)
        {
            return (_context.MenuCategories?.Any(e => e.MenuItemId == id)).GetValueOrDefault();
        }
    }
}
