﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;
using Microsoft.AspNetCore.OData.Query;

namespace AzzanOrder.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherDetailController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public VoucherDetailController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/VoucherDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherDetail>>> GetVoucherDetails(int? employeeId)
        {
            if (_context.VoucherDetails == null)
            {
                return NotFound();
            }
            //var a = await _context.VoucherDetails.Include(vd => vd.Vouchers.Where(v=>v.IsActive == true)).ToListAsync();
            var b = employeeId.HasValue
                ? await _context.MemberVouchers.Where(mv => mv.VoucherDetail.EmployeeId == employeeId).Include(vd => vd.VoucherDetail).ThenInclude(vd => vd.Vouchers.Where(v => v.IsActive == true)).Select(mv => mv.VoucherDetail).ToListAsync()
                : await _context.MemberVouchers.Include(vd => vd.VoucherDetail).ThenInclude(vd => vd.Vouchers.Where(v => v.IsActive == true)).Select(mv => mv.VoucherDetail).ToListAsync();
            return Ok(b);
        }

        [HttpGet("ListVoucherDetail")]
        public async Task<ActionResult<IEnumerable<VoucherDetail>>> GetListVoucherDetails(int? employeeId)
        {
            if (_context.VoucherDetails == null)
            {
                return NotFound();
            }
            
            //var a = await _context.VoucherDetails.Include(vd => vd.Vouchers.Where(v=>v.IsActive == true)).ToListAsync();
            var b = employeeId.HasValue
                ? await _context.VoucherDetails.Where(mv => mv.EmployeeId == employeeId).Include(vd => vd.Vouchers.Where(v => v.IsActive == true)).ToListAsync()
                : await _context.VoucherDetails.Include(vd => vd.Vouchers.Where(v => v.IsActive == true)).ToListAsync();
            return Ok(b);
        }

        [HttpGet("categoryId")]
        public async Task<ActionResult> GetVoucherDetailByCategory(int categoryId, int? employeeId)
        {
            if (_context.VoucherDetails == null)
            {
                return NotFound();
            }
            var voucherDetails = employeeId.HasValue ? _context.Vouchers
    .Where(v => v.ItemCategoryId == categoryId && v.IsActive == true && v.VoucherDetail.EmployeeId == employeeId)  // L?c theo categoryId và IsActive
    .Include(vd => vd.VoucherDetail)
    .ToList() : _context.Vouchers
    .Where(v => v.ItemCategoryId == categoryId && v.IsActive == true)  // L?c theo categoryId và IsActive
    .Include(vd => vd.VoucherDetail)
    .ToList();


            return Ok(voucherDetails);
        }



        // GET: api/VoucherDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherDetail>> GetVoucherDetail(int id)
        {
            if (_context.VoucherDetails == null)
            {
                return NotFound();
            }
            var voucherDetail = await _context.VoucherDetails.Include(vd => vd.Vouchers).FirstOrDefaultAsync(vd => vd.VoucherDetailId == id);

            if (voucherDetail == null)
            {
                return NotFound();
            }

            return Ok(voucherDetail);
        }

        // PUT: api/VoucherDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutVoucherDetail(VoucherDetail voucherDetail)
        {

            if (!VoucherDetailExists(voucherDetail.VoucherDetailId))
            {
                return NotFound("Voucher not exist!");
            }
            _context.Entry(voucherDetail).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherDetailExists(voucherDetail.VoucherDetailId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(_context.VoucherDetails.FirstOrDefault(vd => vd.VoucherDetailId == voucherDetail.VoucherDetailId));
        }

        // POST: api/VoucherDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<VoucherDetail>> PostVoucherDetail(VoucherDetail voucherDetail)
        {
            if (_context.VoucherDetails == null)
            {
                return Problem("List voucher are null.");
            }
            if (voucherDetail.Price == null)
            {
                voucherDetail.Price = 0;
            }
            _context.VoucherDetails.Add(voucherDetail);
            await _context.SaveChangesAsync();


            return Ok(voucherDetail);
        }

        // DELETE: api/VoucherDetail/5
        [HttpDelete("Delete/{voucherDetailId}")]
        public async Task<IActionResult> DeleteVoucherDetail(int voucherDetailId)
        {
            if (_context.VoucherDetails == null)
            {
                return NotFound();
            }
            var voucherDetail = await _context.VoucherDetails.FindAsync(voucherDetailId);
            if (voucherDetail == null)
            {
                return NotFound();
            }
            if (_context.Vouchers.Where(v => v.VoucherDetailId == voucherDetailId).Count() > 0)
            {
                return BadRequest("Delete fail cause have some item using this voucher.");
            }
            _context.VoucherDetails.Remove(voucherDetail);
            await _context.SaveChangesAsync();
            return Ok("Delete success");
        }

        private bool VoucherDetailExists(int id)
        {
            return (_context.VoucherDetails?.Any(e => e.VoucherDetailId == id)).GetValueOrDefault();
        }
    }
}
