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
            return await _context.MemberVouchers.Include(mv => mv.VoucherDetail).ThenInclude(mv => mv.Vouchers).ToListAsync();
        }

        // GET: api/MemberVouchers/5
        [HttpGet("memberId")]
        public async Task<ActionResult> GetMemberVoucher(int memberId)
        {
            if (_context.MemberVouchers == null)
            {
                return NotFound();
            }
            var memberVoucher = await _context.MemberVouchers.Where(mv => mv.MemberId == memberId && mv.IsActive == true).Include(mv => mv.VoucherDetail).ThenInclude(mv => mv.Vouchers).Select(mv => mv.VoucherDetail).ToListAsync();
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

            var memberVoucher = await _context.VoucherDetails.Where(vd => vd.Vouchers.Any(v => v.ItemCategoryId == categoryId) && vd.MemberVouchers.Any(mv => mv.MemberId == memberId && mv.IsActive == true)).Include(vd => vd.Vouchers).ToListAsync();

            return Ok(memberVoucher);


        }

        // PUT: api/MemberVouchers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutMemberVoucher(MemberVoucher memberVoucher)
        {
            if (!MemberVoucherExists(memberVoucher))
            {
                return BadRequest();
            }

            _context.Entry(memberVoucher).State = EntityState.Modified;

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
        public async Task<ActionResult<MemberVoucher>> PostMemberVoucher(MemberVoucher memberVoucher)
        {
            if (_context.MemberVouchers == null)
            {
                return Problem("Entity set 'OrderingAssistSystemContext.MemberVouchers'  is null.");
            }
            //if (MemberVoucherExists(memberVoucher))
            //{
            //    return Conflict("You have this voucher");
            //}
            MemberVoucher mv = new MemberVoucher() { MemberId = memberVoucher.MemberId, VoucherDetailId = memberVoucher.VoucherDetailId, OrderId = memberVoucher.OrderId };
            mv.IsActive = true;
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

            return CreatedAtAction("GetMemberVoucher", new { id = mv.MemberVoucherId }, mv);
        }

        // DELETE: api/MemberVouchers/5
        [HttpDelete("Delete/MemberVoucherId")]
        public async Task<IActionResult> DeleteMemberVoucher(int MemberVoucherId)
        {
            if (_context.MemberVouchers == null)
            {
                return NotFound();
            }
            var mv = await _context.MemberVouchers.FirstOrDefaultAsync(m => m.MemberVoucherId == MemberVoucherId);
            if (mv == null)
            {
                return NotFound();
            }
            if (mv.OrderId != null)
            {
                mv.IsActive = false;
                _context.MemberVouchers.Update(mv);
            }
            _context.MemberVouchers.Remove(mv);
            await _context.SaveChangesAsync();

            return Ok("Remove Success");
        }

        private bool MemberVoucherExists(MemberVoucher memberVoucher)
        {
            return (_context.MemberVouchers?.Any(e => e.MemberId == memberVoucher.MemberId && e.VoucherDetailId == memberVoucher.VoucherDetailId)).GetValueOrDefault();
        }
    }
}
