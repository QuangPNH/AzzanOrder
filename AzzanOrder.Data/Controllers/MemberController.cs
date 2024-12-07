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
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

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
            var member = _context.Members.FirstOrDefault(x => x.MemberId.Equals(id));

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
            var member = _context.Members.FirstOrDefault(m => m.Phone.Equals(phone));

            if (member == null)
            {
                return NotFound();
            }
            return member;
        }

        [HttpGet("Gmail/{gmail}")]
        public async Task<ActionResult<Member>> GetMemberByGmail(string gmail)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = _context.Members.FirstOrDefault(m => m.Gmail.ToLower().Equals(gmail.ToLower()));

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
            string errors = "";
            //^(0)(\d{9})$
            if (!Regex.IsMatch(phone, "^(0)(\\d{9})$"))
            {
                errors += "\nPhone number is invalid";
            }
            var member = _context.Members.FirstOrDefault(m => m.Phone.Equals(phone));

            if (member != null)
            {
                errors += "\nPhone number is already registered";
            }
            if (errors.Length > 0)
            {
                return BadRequest(errors);
            }
            else
            {
                Random random = new Random();
                char[] numbers = new char[6];
                for (int i = 0; i < 6; i++)
                {
                    numbers[i] = (char)('0' + random.Next(0, 10));
                }

                Member member1 = new Member()
                {
                    Phone = phone,
                    MemberName = new string(numbers),
                    Image = "https://cellphones.com.vn/sforum/wp-content/uploads/2023/10/avatar-trang-4.jpg"
                };
                
                //var accountSid = "ACd5083d30edb839433981a766a0c2e2fd";
                //var authToken = "00867f56a886a975463d3ec7941061";
                //TwilioClient.Init(accountSid, authToken);
                //var messageOptions = new CreateMessageOptions(new PhoneNumber("+84388536414"));
                //messageOptions.From = new PhoneNumber("+19096555985");
                //messageOptions.Body = "Your OTP is " + new string(numbers);
                //var message = MessageResource.Create(messageOptions);
                //Console.WriteLine(message.Body);
                return Ok(member1);
            }
        }













        // PUT: api/Member/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutMember(Member member)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'OrderingAssistSystemContext.Members' is null.");
            }

            _context.Members.Update(member);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(member.MemberId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(member);
        }

        [HttpGet("UpdatePoints/memberId/point")]
        public async Task<IActionResult> UpdatePoints(int memberId, double point)
        {
            var member = _context.Members.FirstOrDefault(x => x.MemberId.Equals(memberId));
            if (member == null)
            {
                return NotFound();
            }

            member.Point += point; // Assuming the member has a Points property
            await _context.SaveChangesAsync();

            return Ok(member);
        }

        // POST: api/Member
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'AzzanOrderContext.Members'  is null.");
            }
            member.IsDelete = false;
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return Ok(member);
        }

        // DELETE: api/Owner/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = _context.Members.FirstOrDefault(x => x.MemberId.Equals(id));
            if (member == null)
            {
                return NotFound();
            }
            member.IsDelete = true;
            _context.Members.Update(member);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        [HttpPut("ImageAdd/{id}")]
        public async Task<IActionResult> PutImage(int id,[FromBody] string img)
        {
            var member = _context.Members.FirstOrDefault(x => x.MemberId.Equals(id));
            if (member == null)
            {
                return NotFound();
            }
            member.Image = img;
            await _context.SaveChangesAsync();
            return Ok();
        }
        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
