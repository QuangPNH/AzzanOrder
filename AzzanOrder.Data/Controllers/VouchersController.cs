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
    public class VouchersController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public VouchersController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Vouchers
        [HttpGet]
        public async Task<ActionResult> GetVouchers(int? id)
        {
            if (_context.Vouchers == null)
            {
                return NotFound("List voucher is empty");
            }            
            var vouchers = await _context.VoucherDetails.Include(v => v.Vouchers).ThenInclude(ic => ic.ItemCategory).ToListAsync();
            return Ok(vouchers);
        }
        [HttpGet("CategoryId")]
        public async Task<IActionResult> GetVoucherByCategory(int categoryId)
        {
            if (_context.Vouchers == null)
            {
                return NotFound("List voucher is empty");
            }
            var vouchers = await _context.Vouchers.Where(v => v.ItemCategoryId == categoryId && v.IsActive == true).Select(v => new { v.ItemCategoryId, v.VoucherDetailId, v.IsActive }).ToListAsync();
            return Ok(vouchers);
        }

        // PUT: api/Vouchers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutVoucher(VoucherDTO voucher)
        {
            if (!VoucherExists(voucher))
            {
                return NotFound("This voucher not exist");
            }
            var vou = new Voucher() { IsActive = voucher.IsActive, ItemCategoryId = voucher.ItemCategoryId, VoucherDetailId = voucher.VoucherDetailId };
            _context.Entry(vou).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherExists(voucher))
                {
                    return NotFound("This voucher not exist");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Update Success");
        }

        // POST: api/Vouchers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult> PostVoucher(VoucherDTO voucher)
        {
            if (_context.Vouchers == null)
            {
                return Problem("List vouchers are null.");
            }
            var vd = _context.VoucherDetails.FirstOrDefault(vd => vd.VoucherDetailId == voucher.VoucherDetailId);
            var ic = _context.ItemCategories.FirstOrDefault(ic => ic.ItemCategoryId == voucher.ItemCategoryId);
            if (vd == null || ic == null)
            {
                return Problem("This voucher not exist.");
            }
            var count = _context.Vouchers.Count();
            Voucher v = new Voucher() { ItemCategoryId = voucher.ItemCategoryId, VoucherDetailId = voucher.VoucherDetailId, IsActive = true };
            _context.Vouchers.Add(v);
            await _context.SaveChangesAsync();
            if(!(_context.Vouchers.Count() > count))
            {
                return Problem("Add fails");
            }
            return Ok(voucher);
        }

        // DELETE: api/Vouchers/5
        [HttpDelete("Delete/Voucher")]
        public async Task<IActionResult> DeleteVoucher([Bind("VoucherDetailId", "ItemCategoryId")] VoucherDTO voucher)
        {
            if (_context.Vouchers == null)
            {
                return NotFound();
            }
            if(!VoucherExists(voucher))
            { 
                return NotFound();
            }
            var v = _context.Vouchers.FirstOrDefault(v => v.VoucherDetailId == voucher.VoucherDetailId && v.ItemCategoryId == voucher.ItemCategoryId);
            _context.Vouchers.Remove(v);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        private bool VoucherExists(VoucherDTO voucher)
        {
            return (_context.Vouchers?.Any(e => e.VoucherDetailId == voucher.VoucherDetailId && e.ItemCategoryId == voucher.ItemCategoryId)).GetValueOrDefault();
        }
    }
}
