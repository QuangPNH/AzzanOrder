using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.DTO;
using AzzanOrder.Data.Models;
using System.Text.Json;

namespace AzzanOrder.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginDTOesController : ControllerBase
    {
        private readonly AzzanOrderContext _context;

        public LoginDTOesController(AzzanOrderContext context)
        {
            _context = context;
        }

        // GET: api/LoginDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginDTO>>> GetLoginDTO()
        {
          if (_context.LoginDTO == null)
          {
              return NotFound();
          }
            return await _context.LoginDTO.ToListAsync();
        }

        // GET: api/LoginDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginDTO>> GetLoginDTO(string id)
        {
            if (_context.LoginDTO == null)
            {
                return NotFound();
            }
            var loginDTO = await _context.LoginDTO.FindAsync(id);

            if (loginDTO == null)
            {
                return NotFound();
            }

            var memberJson = JsonSerializer.Serialize(loginDTO);

            return Content(memberJson, "application/json");
        }
        // GET: api/Members/{phoneNumber}
        [HttpGet("Members/{phoneNumber}")]
        public async Task<ActionResult<Member>> GetMemberByPhoneNumber(string phoneNumber)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Phone == phoneNumber);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }
        
        // PUT: api/LoginDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginDTO(string id, LoginDTO loginDTO)
        {
            if (id != loginDTO.id)
            {
                return BadRequest();
            }

            _context.Entry(loginDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginDTOExists(id))
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

        // POST: api/LoginDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoginDTO>> PostLoginDTO(LoginDTO loginDTO)
        {
          if (_context.LoginDTO == null)
          {
              return Problem("Entity set 'AzzanOrderContext.LoginDTO'  is null.");
          }
            _context.LoginDTO.Add(loginDTO);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoginDTOExists(loginDTO.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLoginDTO", new { id = loginDTO.id }, loginDTO);
        }

        // DELETE: api/LoginDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoginDTO(string id)
        {
            if (_context.LoginDTO == null)
            {
                return NotFound();
            }
            var loginDTO = await _context.LoginDTO.FindAsync(id);
            if (loginDTO == null)
            {
                return NotFound();
            }

            _context.LoginDTO.Remove(loginDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginDTOExists(string id)
        {
            return (_context.LoginDTO?.Any(e => e.id == id)).GetValueOrDefault();
        }
        // POST: api/LoginDTOes/LoginByPhone
        [HttpPost("LoginByPhone")]
        public async Task<ActionResult<Member>> LoginByPhone(string phoneNumber)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Phone == phoneNumber);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }
    }
}
