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
        [HttpGet("GetAllMenuItem")]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems(int? employeeId)
        {
            if (_context.MenuItems == null)
            {
                return NotFound();
            }
            var a = employeeId.HasValue ? await _context.MenuItems.Include(mi => mi.MenuCategories).ThenInclude(mi=>mi.ItemCategory).Where(mi => mi.EmployeeId == employeeId).ToListAsync() : await _context.MenuItems.Include(mi => mi.MenuCategories).ThenInclude(mi => mi.ItemCategory).ToListAsync();
            return a;
        }

        [HttpGet("ItemCategoryId/{itemCategoryId}")]
        public async Task<ActionResult> GetMenuItemByTtemCategory(int itemCategoryId)
        {
            if (_context.MenuItems == null)
            {
                return NotFound();
            }
            var b = await _context.MenuCategories.Include(mi => mi.MenuItem).Where(mi => mi.ItemCategoryId == itemCategoryId && mi.MenuItem.IsAvailable == true).ToListAsync();

            //var a = employeeId.HasValue ? await _context.MenuItems.Include(mi => mi.MenuCategories).ThenInclude(mi => mi.ItemCategory).Where(mi => mi.EmployeeId == employeeId).ToListAsync() : await _context.MenuItems.Include(mi => mi.MenuCategories).ThenInclude(mi => mi.ItemCategory).ToListAsync();
            return Ok(b);
        }
        // GET: api/MenuItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
            if (_context.MenuItems == null)
            {
                return NotFound();
            }
            var menuItem = await _context.MenuItems.Include(mi => mi.MenuCategories).ThenInclude(mi => mi.ItemCategory).FirstOrDefaultAsync(mi => mi.MenuItemId == id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return menuItem;
        }

        // PUT: api/MenuItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutMenuItem(MenuItem menuItem)
        {

           
                if (!MenuItemExists(menuItem.MenuItemId)){
                    return NotFound();
                }
                _context.Entry(menuItem).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.MenuItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(_context.MenuItems.FirstOrDefault(vd => vd.MenuItemId == menuItem.MenuItemId));
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
            MenuItem mi = new MenuItem()
            {
                ItemName = menuItem.ItemName,
                Price = menuItem.Price,
                Description = menuItem.Description,
                Discount = menuItem.Discount,
                Image = menuItem.Image,       
                IsAvailable = true,
                EmployeeId = menuItem.EmployeeId
            };
            
            _context.MenuItems.Add(mi);
            await _context.SaveChangesAsync();

            return Ok(mi);
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
                    (mc.ItemCategory.EndDate == null && mc.ItemCategory.StartDate == null) ||
                    (mc.ItemCategory.EndDate > DateTime.Now && mc.ItemCategory.StartDate < DateTime.Now && mc.ItemCategory.IsCombo == false)))
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
                (mc.ItemCategory.EndDate == null && mc.ItemCategory.StartDate == null ||
                mc.ItemCategory.EndDate > DateTime.Now && mc.ItemCategory.StartDate < DateTime.Now && mc.ItemCategory.IsCombo == false)) &&
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
                    (mc.ItemCategory.EndDate == null && mc.ItemCategory.StartDate == null ||
                    mc.ItemCategory.EndDate > DateTime.Now && mc.ItemCategory.StartDate < DateTime.Now && mc.ItemCategory.IsCombo == false)) &&
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

		// GET: api/MenuItem/Sales
		[HttpGet("Sales/{id}/{startDate}/{endDate}")]
		public async Task<ActionResult<IEnumerable<MenuItemSalesDTO>>> GetMenuItemSales(int id, string startDate, string endDate)
		{
			var menuItems = await _context.MenuItems
                .Include(mi => mi.OrderDetails)
                .ThenInclude(od => od.Order)
                .Where(mi => mi.IsAvailable == true && mi.Employee.OwnerId == id && mi.Employee.IsDelete != true)
                .Select(mi => new
                {
                    mi.MenuItemId,
                    mi.ItemName,
                    mi.Employee.EmployeeName,
                    OrderDetails = mi.OrderDetails
                    .Where(od => od.Order.OrderDate >= DateTime.Parse(startDate) && od.Order.OrderDate <= DateTime.Parse(endDate))
                    .Select(od => od.Quantity)
                })
                .ToListAsync();

			var groupedMenuItems = menuItems
				.GroupBy(mi => mi.ItemName)
				.Select(g => new MenuItemSalesDTO
				{
					MenuItemId = g.First().MenuItemId,
					ItemName = g.Key,
					Sales = (int)g.Sum(mi => mi.OrderDetails.Sum()),
					ManagerName = g.First().EmployeeName
				})
				.OrderByDescending(mi => mi.Sales)
				.ToList();

			return Ok(groupedMenuItems);
		}

		[HttpGet("ManagerSales/{id}/{startDate}/{endDate}")]
		public async Task<ActionResult<IEnumerable<MenuItemSalesDTO>>> GetSales(int id, string startDate, string endDate)
		{
			var menuItems = await _context.MenuItems
				.Include(mi => mi.OrderDetails)
				.ThenInclude(od => od.Order)
				.Where(mi => mi.IsAvailable == true && mi.Employee.OwnerId == id && mi.Employee.IsDelete != true)
				.Select(mi => new
				{
					mi.MenuItemId,
					mi.ItemName,
					mi.Employee.EmployeeName,
					OrderDetails = mi.OrderDetails
					.Where(od => od.Order.OrderDate >= DateTime.Parse(startDate) && od.Order.OrderDate <= DateTime.Parse(endDate))
					.Select(od => od.Quantity)
				})
				.ToListAsync();

			var groupedMenuItems = menuItems
				.GroupBy(mi => mi.EmployeeName)
				.Select(g => new MenuItemSalesDTO
				{
					Sales = (int)g.Sum(mi => mi.OrderDetails.Sum()),
					ManagerName = g.First().EmployeeName
				})
				.OrderByDescending(mi => mi.Sales)
				.ToList();

			return Ok(groupedMenuItems);
		}
	}
}
