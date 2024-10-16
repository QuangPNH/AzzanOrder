using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;

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
        public async Task<ActionResult<IEnumerable<VoucherDetail>>> GetVoucherDetails()
        {
          if (_context.VoucherDetails == null)
          {
              return NotFound();
          }
            return await _context.VoucherDetails.ToListAsync();
        }

        // GET: api/VoucherDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherDetail>> GetVoucherDetail(int id)
        {
          if (_context.VoucherDetails == null)
          {
              return NotFound();
          }
            var voucherDetail = await _context.VoucherDetails.FindAsync(id);

            if (voucherDetail == null)
            {
                return NotFound();
            }

            return Ok(voucherDetail);
        }

        // PUT: api/VoucherDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutVoucherDetail([Bind("VoucherDetailId", "Title", "StartDate", "EndDate", "Discount")] VoucherDetail voucherDetail)
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
            return Ok(_context.VoucherDetails.FirstOrDefault(vd=>vd.VoucherDetailId == voucherDetail.VoucherDetailId));
        }

        // POST: api/VoucherDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<VoucherDetail>> PostVoucherDetail([Bind("Title", "StartDate", "EndDate", "Discount")] VoucherDetail voucherDetail)
        {
          if (_context.VoucherDetails == null)
          {
              return Problem("List voucher are null.");
          }
            _context.VoucherDetails.Add(voucherDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoucherDetail", new { id = voucherDetail.VoucherDetailId }, voucherDetail);
        }

        // DELETE: api/VoucherDetail/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteVoucherDetail(int id)
        {
            if (_context.VoucherDetails == null)
            {
                return NotFound();
            }
            var voucherDetail = await _context.VoucherDetails.FindAsync(id);
            if (voucherDetail == null)
            {
                return NotFound();
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
