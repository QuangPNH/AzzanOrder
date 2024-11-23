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
        public async Task<ActionResult<IEnumerable<MenuCategory>>> GetMenuCategories(int ?employeeId)
        {
          if (_context.MenuCategories == null)
          {
              return NotFound();
          }
            var a = employeeId.HasValue ?  await _context.MenuCategories.Include(mc=>mc.ItemCategory).Where(mc => mc.MenuItem.EmployeeId == employeeId).ToListAsync() : await _context.MenuCategories.Include(mc => mc.ItemCategory).ToListAsync();
            return a ;
        }

        //GET: api/MenuCategory/5
      
        [HttpGet("itemCategoryId/menuItemId")]
        public async Task<ActionResult> CheckVoucher(int itemCategoryId, int menuItemId)
        {
            if (_context.MenuCategories == null)
            {
                return NotFound("List voucher is empty");
            }

            var mC = await _context.MenuCategories.FirstOrDefaultAsync(mc => mc.MenuItemId == menuItemId && mc.ItemCategoryId == itemCategoryId);
            if(mC == null)
            {
                return NotFound() ;
            }
            //var vouchers = await _context.VoucherDetails.Include(v => v.Vouchers).ThenInclude(ic => ic.ItemCategory).ToListAsync();
            return Ok(mC);
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
          var menuC = new MenuCategory() { MenuItemId = menuCategory.MenuItemId, ItemCategoryId = menuCategory.ItemCategoryId};
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

            return Ok("Delete success");
        }

        private bool MenuCategoryExists(MenuCategoryDTO menuCategory)
        {
            return (_context.MenuCategories?.Any(e => e.MenuItemId == menuCategory.MenuItemId && e.ItemCategoryId == menuCategory.ItemCategoryId)).GetValueOrDefault();
        }
    }
}
