using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;
using Newtonsoft.Json;

namespace AzzanOrder.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public BankController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Bank
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {
            if (_context.Banks == null)
            {
                return NotFound();
            }
            return await _context.Banks.ToListAsync();
        }

        [HttpGet("AllBank")]
        public async Task<ActionResult> GetAllBank()
        {
            if (_context.Banks == null)
            {
                return NotFound();
            }
            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                var htmlData = await httpClient.GetStringAsync("https://api.vietqr.io/v2/banks");
                var listBankData = JsonConvert.DeserializeObject<Api.Bank>(htmlData);
                return Ok(listBankData);
            }
        }
        // GET: api/Bank/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            if (_context.Banks == null)
            {
                return NotFound();
            }
            var bank = await _context.Banks.FindAsync(id);

            if (bank == null)
            {
                return NotFound();
            }

            return bank;
        }

        // PUT: api/Bank/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutBank(Bank bank)
        {
            if (!BankExists(bank.BankId))
            {
                return NotFound("This bank not exist.");
            }

            _context.Entry(bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(bank.BankId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(_context.Banks.FirstOrDefault(a => a.BankId == bank.BankId));
        }

        // POST: api/Bank
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            if (_context.Banks == null)
            {
                return Problem("Bank is null.");
            }
            var b = new Bank() { BankName = bank.BankName, BankNumber = bank.BankNumber };
            _context.Banks.Add(b);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBank", new { id = b.BankId }, b);
        }

        // DELETE: api/Bank/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            if (_context.Banks == null)
            {
                return NotFound();
            }
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();

            return Ok("Delete Success");
        }

        private bool BankExists(int id)
        {
            return (_context.Banks?.Any(e => e.BankId == id)).GetValueOrDefault();
        }
    }
}
//Tuan da o day