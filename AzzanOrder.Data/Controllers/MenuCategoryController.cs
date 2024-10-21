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
        [HttpPut("Update")]
        public async Task<IActionResult> PutMenuCategory(MenuCategoryDTO menuCategory)
        {
            if (!MenuCategoryExists(menuCategory))
            {
                return NotFound();
            }
            var menuC = _context.MenuCategories.FirstOrDefault(mc => mc.MenuItemId == menuCategory.MenuItemId && mc.ItemCategoryId == menuCategory.ItemCategoryId);
            _context.Entry(menuC).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuCategoryExists(menuCategory))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(menuC);
        }

        // POST: api/MenuCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult> PostMenuCategory(MenuCategoryDTO menuCategory)
        {
          if (_context.MenuCategories == null)
          {
              return Problem("Category is null.");
          }
          var menuC = new MenuCategory() { MenuItemId = menuCategory.MenuItemId, ItemCategoryId = menuCategory.ItemCategoryId, StartDate = menuCategory.StartDate, EndDate = menuCategory.EndDate, IsForCombo = menuCategory.IsForCombo };
            _context.MenuCategories.Add(menuC);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MenuCategoryExists(menuCategory))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.MenuCategories.FirstOrDefault(mc => mc.MenuItemId == menuC.MenuItemId && mc.ItemCategoryId == menuC.ItemCategoryId));
        }

        // DELETE: api/MenuCategory/5
        [HttpDelete("Delete/MenuCategory")]
        public async Task<IActionResult> DeleteMenuCategory(MenuCategoryDTO menuCategory)
        {
            if (_context.MenuCategories == null)
            {
                return NotFound();
            }
            var menuC = await _context.MenuCategories.FirstOrDefaultAsync(mc=>mc.ItemCategoryId == menuCategory.ItemCategoryId && mc.ItemCategoryId == menuCategory.ItemCategoryId);
            if (menuC == null)
            {
                return NotFound();
            }
            _context.MenuCategories.Remove(menuC);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuCategoryExists(MenuCategoryDTO menuCategory)
        {
            return (_context.MenuCategories?.Any(e => e.MenuItemId == menuCategory.MenuItemId && e.ItemCategoryId == menuCategory.ItemCategoryId)).GetValueOrDefault();
        }
    }
}
