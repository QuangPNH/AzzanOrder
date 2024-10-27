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
    public class MemberVouchersController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public MemberVouchersController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/MemberVouchers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberVoucher>>> GetMemberVouchers()
        {
            if (_context.MemberVouchers == null)
            {
                return NotFound();
            }
            return await _context.MemberVouchers.ToListAsync();
        }

        // GET: api/MemberVouchers/5
        [HttpGet("memberId")]
        public async Task<ActionResult> GetMemberVoucher(int memberId)
        {
            if (_context.MemberVouchers == null)
            {
                return NotFound();
            }
            var memberVoucher = await _context.MemberVouchers.Where(mv => mv.MemberId == memberId && mv.IsActive == true).Select(mv => mv.VoucherDetail).ToListAsync();

            if (memberVoucher == null)
            {
                return NotFound();
            }

            return Ok(memberVoucher);
        }

        [HttpGet("memberId/itemCategoryId")]
        public async Task<ActionResult> GetMemberVoucherByMemberAndCategory(int memberId, int categoryId)
        {
            if (_context.MemberVouchers == null)
            {
                return BadRequest();
            }
           
            var memberVoucher = await _context.VoucherDetails.Where(vd => vd.Vouchers.Any(v => v.ItemCategoryId == categoryId) && vd.MemberVouchers.Any(mv => mv.MemberId == memberId && mv.IsActive == true)).ToListAsync();
            
           return Ok(memberVoucher);
           

        }

        // PUT: api/MemberVouchers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutMemberVoucher(MemberVoucherDTO memberVoucher)
        {
            if (!MemberVoucherExists(memberVoucher))
            {
                return BadRequest();
            }
            var mv = new MemberVoucher() { MemberId = memberVoucher.MemberId, VoucherDetailId = memberVoucher.VoucherDetailId, IsActive = memberVoucher.IsActive, OrderId = memberVoucher.OrderId };
            _context.Entry(mv).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberVoucherExists(memberVoucher))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(memberVoucher);
        }

        // POST: api/MemberVouchers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<MemberVoucher>> PostMemberVoucher(MemberVoucherDTO memberVoucher)
        {
            if (_context.MemberVouchers == null)
            {
                return Problem("Entity set 'OrderingAssistSystemContext.MemberVouchers'  is null.");
            }
            if (MemberVoucherExists(memberVoucher))
            {
                return Conflict("You have this voucher");
            }
            MemberVoucher mv = new MemberVoucher() { MemberId = memberVoucher.MemberId, VoucherDetailId = memberVoucher.VoucherDetailId, IsActive = memberVoucher.IsActive, OrderId = memberVoucher.OrderId };
            _context.MemberVouchers.Add(mv);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemberVoucherExists(memberVoucher))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMemberVoucher", new { id = memberVoucher.MemberId, memberVoucher.VoucherDetailId }, memberVoucher);
        }

        // DELETE: api/MemberVouchers/5
        [HttpDelete("Delete/MemberVoucher")]
        public async Task<IActionResult> DeleteMemberVoucher(MemberVoucherDTO memberVoucher)
        {
            if (_context.MemberVouchers == null)
            {
                return NotFound();
            }
            var mv = await _context.MemberVouchers.FirstOrDefaultAsync(m => m.VoucherDetailId == memberVoucher.VoucherDetailId && m.MemberId == memberVoucher.MemberId);
            if (mv == null)
            {
                return NotFound();
            }
            if(mv.OrderId != null)
            {
                return Conflict("This not be delete");
            }
            _context.MemberVouchers.Remove(mv);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberVoucherExists(MemberVoucherDTO memberVoucher)
        {
            return (_context.MemberVouchers?.Any(e => e.MemberId == memberVoucher.MemberId && e.VoucherDetailId == memberVoucher.VoucherDetailId)).GetValueOrDefault();
        }
    }
}
