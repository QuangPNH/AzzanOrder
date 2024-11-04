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
    public class MenuItemController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public MenuItemController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/MenuItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
        {
            if (_context.MenuItems == null)
            {
                return NotFound();
            }
            return await _context.MenuItems.ToListAsync();
        }

        // GET: api/MenuItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
            if (_context.MenuItems == null)
            {
                return NotFound();
            }
            var menuItem = await _context.MenuItems.FindAsync(id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return menuItem;
        }

        // PUT: api/MenuItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItem(int id, MenuItemAddDTO menuItem)
        {
            
            try
            {
                var menuItemToUpdate = await _context.MenuItems.FindAsync(id);
                if (!MenuItemExists(id))
                {
                    return NotFound();
                }
                menuItemToUpdate.ItemName = menuItem.ItemName;
                menuItemToUpdate.Price = menuItem.Price;
                menuItemToUpdate.Description = menuItem.Description;
                menuItemToUpdate.Discount = menuItem.Discount;
                menuItemToUpdate.Image = menuItem.ImageBase64;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return Ok(menuItem);
        }

        // POST: api/MenuItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
        {
            if (_context.MenuItems == null)
            {
                return Problem("Entity set 'OrderingAssistSystemContext.MenuItems'  is null.");
            }
            menuItem.IsAvailable = true;
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuItem", new { id = menuItem.MenuItemId }, menuItem);
        }

        // DELETE: api/MenuItem/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            if (_context.MenuItems == null)
            {
                return NotFound();
            }
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            menuItem.IsAvailable = false;
            //foreach(var menuCategory in menuItem.MenuCategories)
            //{
            //    _context.MenuCategories.Remove(menuCategory);
            //}

            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        private bool MenuItemExists(int id)
        {
            return (_context.MenuItems?.Any(e => e.MenuItemId == id)).GetValueOrDefault();
        }
        // GET: api/MenuItem/Top4
        [HttpGet("Top4")]
        public async Task<ActionResult<IEnumerable<MenuItemDTO>>> GetTop4MenuItems(int? id)
        {
            var query = _context.MenuItems.Where(m => m.IsAvailable == true);

            if (id.HasValue)
            {
                query = query.Where(m => m.EmployeeId == id.Value);
            }

            var top4MenuItems = await query
                .Where(m => m.MenuCategories.Any(mc =>
                    (mc.EndDate == null && mc.StartDate == null) ||
                    (mc.EndDate > DateTime.Now && mc.StartDate < DateTime.Now && mc.IsForCombo == false)))
                .OrderByDescending(m => m.OrderDetails.Count)
                .Take(4)
                .Select(m => new MenuItemDTO
                {
                    MenuItemId = m.MenuItemId,
                    ItemName = m.ItemName,
                    Price = m.Price,
                    Description = m.Description,
                    Discount = m.Discount,
                    Category = m.MenuCategories.FirstOrDefault().ItemCategory.Description,
                    ImageBase64 = m.Image,
                    EmployeeId = m.EmployeeId
                })
                .ToListAsync();

            if (top4MenuItems == null || !top4MenuItems.Any())
            {
                return NotFound();
            }

            return Ok(top4MenuItems);
        }
        // GET: api/MenuItem/Category/{category}
        [HttpGet("Category/{categoryname}")]
        public async Task<ActionResult<IEnumerable<MenuItemDTO>>> GetMenuItemsByCategory(string categoryname, int? id)
        {
            var query = _context.MenuItems.Where(m => m.MenuCategories.Any(mc =>
                mc.ItemCategory.Description.ToLower().Contains(categoryname.ToLower()) &&
                (mc.EndDate == null && mc.StartDate == null ||
                mc.EndDate > DateTime.Now && mc.StartDate < DateTime.Now && mc.IsForCombo == false)) &&
                m.IsAvailable == true);

            if (id.HasValue)
            {
                query = query.Where(m => m.EmployeeId == id.Value);
            }

            var menuItems = await query
                .Select(m => new MenuItemDTO
                {
                    MenuItemId = m.MenuItemId,
                    ItemName = m.ItemName,
                    Price = m.Price,
                    Category = m.MenuCategories.FirstOrDefault().ItemCategory.Description,
                    Description = m.Description,
                    Discount = m.Discount,
                    ImageBase64 = m.Image,
                    EmployeeId = m.EmployeeId
                })
                .ToListAsync();

            if (menuItems == null || !menuItems.Any())
            {
                return NotFound();
            }

            return Ok(menuItems);
        }

        // GET: api/MenuItem/RecentMenuItems/{customerId}
        [HttpGet("RecentMenuItems/{customerId}")]
        public async Task<ActionResult<IEnumerable<MenuItemDTO>>> GetRecentMenuItems(int customerId, int? id)
        {
            var query = _context.OrderDetails
                .Where(od => od.Order.MemberId == customerId)
                .OrderByDescending(od => od.Order.OrderDate)
                .Select(od => od.MenuItem)
                .Distinct()
                .Take(4)
                .Where(m => m.MenuCategories.Any(mc =>
                    (mc.EndDate == null && mc.StartDate == null ||
                    mc.EndDate > DateTime.Now && mc.StartDate < DateTime.Now && mc.IsForCombo == false)) &&
                    m.IsAvailable == true);

            if (id.HasValue)
            {
                query = query.Where(m => m.EmployeeId == id.Value);
            }

            var recentMenuItems = await query
                .Select(m => new MenuItemDTO
                {
                    MenuItemId = m.MenuItemId,
                    ItemName = m.ItemName,
                    Price = m.Price,
                    Description = m.Description,
                    Category = m.MenuCategories.FirstOrDefault().ItemCategory.Description,
                    Discount = m.Discount,
                    ImageBase64 = m.Image,
                    EmployeeId = m.EmployeeId
                })
                .ToListAsync();

            if (recentMenuItems == null || !recentMenuItems.Any())
            {
                return NotFound();
            }

            return Ok(recentMenuItems);
        }
    }
}
