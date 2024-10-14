using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AzzanOrder.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public MemberController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Member
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            return await _context.Members.ToListAsync();
        }

        // GET: api/Member/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // GET: api/Member/5
        [HttpGet("Phone/{phone}")]
        public async Task<ActionResult<Member>> GetMemberByPhone(string phone)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Phone.Equals(phone));

            if (member == null)
            {
                return NotFound();
            }
            return member;
        }










        //Register!!!!!
        [HttpGet("Register/{phone}")]
        public async Task<ActionResult<Member>> Register(string phone)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Phone.Equals(phone));

            if (member == null || _context.Members == null)
            {
                Random random = new Random();
                char[] numbers = new char[6];

                for (int i = 0; i < 6; i++)
                {
                    // Generate a random number between 0 and 9
                    numbers[i] = (char)('0' + random.Next(0, 10));
                }

                Member member1 = new Member()
                {
                    Phone = phone,
                    MemberName = new string(numbers)
                };

                var accountSid = "ACd5083d30edb839433981a766a0c2e2fd";
                var authToken = "a7006676dd3f015b9b47177e3f333852";
                TwilioClient.Init(accountSid, authToken);
                var messageOptions = new CreateMessageOptions(new PhoneNumber("+84388536414"));
                messageOptions.From = new PhoneNumber("+19096555985");
                messageOptions.Body = "Your OTP is " + new string(numbers);
                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);

                return Ok(member1);
            }
            else
            return Conflict("Phone number is already registered");
        }













        // PUT: api/Member/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Member
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'AzzanOrderContext.Members'  is null.");
            }
            member.MemberName = null;
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
        }

        // DELETE: api/Member/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
