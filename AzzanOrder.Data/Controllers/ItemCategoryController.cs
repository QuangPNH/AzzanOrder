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
        public async Task<ActionResult<IEnumerable<ItemCategory>>> GetItemCategories(int? id)
        {
            if (_context.ItemCategories == null)
            {
                return NotFound();
            }

            var query = _context.ItemCategories
                .Where(ic => !ic.Description.Contains("TOPPING"));

            if (id.HasValue)
            {
                query = query.Where(ic =>
                ic.EmployeeId == id.Value ||
                ic.MenuCategories.Any(mc => mc.MenuItem.EmployeeId == id.Value));
            }

            var itemCategories = await query
                .Include(ic => ic.MenuCategories)
                .ThenInclude(mc => mc.MenuItem)
                .ToListAsync();

            if (itemCategories == null || !itemCategories.Any())
            {
                return NotFound();
            }

            // Remove MenuCategories that don't have MenuItems with the specified EmployeeId
            if (id.HasValue)
            {
                foreach (var itemCategory in itemCategories)
                {
                    itemCategory.MenuCategories = itemCategory.MenuCategories
                        .Where(mc => mc.MenuItem.EmployeeId == id.Value)
                        .ToList();
                }
            }

            return Ok(itemCategories);
        }

        [HttpGet("GetByManagerId/{id}")]
        public async Task<ActionResult<IEnumerable<ItemCategory>>> GetByManagerId(int? id)
        {
            if (_context.ItemCategories == null)
            {
                return NotFound();
            }
            return await _context.ItemCategories.Where(x => x.EmployeeId == 1).ToListAsync();
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

        [HttpGet("VoucherDetailId")]
        public async Task<ActionResult<IEnumerable>> GetItemCategoryByVoucherDetail(int VoucherDetailId)
        {
            if (_context.ItemCategories == null)
            {
                return NotFound();
            }
            var voucherDetails = _context.Vouchers
            .Where(v => v.VoucherDetailId == VoucherDetailId && v.IsActive == true)
            .Include(vd => vd.ItemCategory)
            .ToList();

            return voucherDetails;
        }

        // PUT: api/ItemCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutItemCategory(ItemCategory itemCategory)
        {
            var ic = await _context.ItemCategories.FindAsync(itemCategory.ItemCategoryId);
            if (ic == null)
            {
                return NotFound();
            }
            ic.ItemCategoryName = itemCategory.ItemCategoryName;
            ic.Description = itemCategory.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCategoryExists(itemCategory.ItemCategoryId))
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
        [HttpPost("Add")]
        public async Task<ActionResult<ItemCategory>> PostItemCategory(ItemCategory itemCategory)
        {
          if (_context.ItemCategories == null)
          {
              return Problem("Entity set 'OrderingAssistSystemContext.ItemCategories'  is null.");
          }
          ItemCategory ic = new ItemCategory() 
          {
              ItemCategoryName = itemCategory.ItemCategoryName, 
              Description = itemCategory.Description, 
              Discount = itemCategory.Discount, 
              Image = itemCategory.Image,
              EmployeeId = itemCategory.EmployeeId,
              IsDelete = itemCategory.IsDelete
          };
            _context.ItemCategories.Add(ic);
            await _context.SaveChangesAsync();

            return Ok();
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
            itemCategory.IsDelete = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemCategoryExists(int id)
        {
            return (_context.ItemCategories?.Any(e => e.ItemCategoryId == id)).GetValueOrDefault();
        }
    }
}
