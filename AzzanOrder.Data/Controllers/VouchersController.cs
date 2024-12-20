﻿using System;
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
        [HttpGet]
        public async Task<ActionResult> GetAllVoucher()
        {
            if (_context.Vouchers == null)
            {
                return NotFound("List voucher is empty");
            }
            var vouchers = await _context.Vouchers.Include(v => v.ItemCategory).ThenInclude(v => v.MenuCategories).ToListAsync();
            return Ok(vouchers);
        }
        // GET: api/Vouchers
        [HttpGet("voucherDetailId/menuItemId")]
        public async Task<ActionResult> CheckVoucher(int voucherDetailid, int menuItemId, int? employeeId)
        {
            if (_context.Vouchers == null)
            {
                return NotFound("List voucher is empty");
            }

            var vouchers = employeeId.HasValue
                ? await _context.Vouchers.Where(v => v.VoucherDetailId == voucherDetailid && v.VoucherDetail.EmployeeId == employeeId).Include(v => v.ItemCategory).ThenInclude(v => v.MenuCategories).ToListAsync()
                : await _context.Vouchers.Where(v => v.VoucherDetailId == voucherDetailid).Include(v => v.ItemCategory).ThenInclude(v => v.MenuCategories).ToListAsync();
            var check = vouchers.Any(v => v.ItemCategory.MenuCategories.Any(m => m.MenuItemId == menuItemId));
            //var vouchers = await _context.VoucherDetails.Include(v => v.Vouchers).ThenInclude(ic => ic.ItemCategory).ToListAsync();
            return Ok(check);
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
            //_context.Entry(vou).State = EntityState.Modified;
            _context.Vouchers.Update(vou);
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
            Voucher v = new Voucher() { ItemCategoryId = voucher.ItemCategoryId, VoucherDetailId = voucher.VoucherDetailId, IsActive = voucher.IsActive };
            _context.Vouchers.Add(v);
            await _context.SaveChangesAsync();
            if (!(_context.Vouchers.Count() > count))
            {
                return Problem("Add fails");
            }
            return Ok(voucher);
        }

        // DELETE: api/Vouchers/5
        [HttpDelete("Delete/{VoucherDetailId}")]
        public async Task<IActionResult> DeleteVoucher(int voucherDetailId)
        {
            if (_context.Vouchers == null)
            {
                return NotFound();
            }
            if (_context.Vouchers.Where(v => v.VoucherDetailId == voucherDetailId).Count() < 0)
            {
                return NotFound();
            }
            foreach (var i in await _context.ItemCategories.ToListAsync())
            {
                var v = _context.Vouchers.FirstOrDefault(v => v.VoucherDetailId == voucherDetailId && v.ItemCategoryId == i.ItemCategoryId);
                v.IsActive = false;
                _context.Vouchers.Update(v);
                await _context.SaveChangesAsync();
            }
            return Ok("Delete success");
        }

        private bool VoucherExists(VoucherDTO voucher)
        {
            return (_context.Vouchers?.Any(e => e.VoucherDetailId == voucher.VoucherDetailId && e.ItemCategoryId == voucher.ItemCategoryId)).GetValueOrDefault();
        }
    }
}
