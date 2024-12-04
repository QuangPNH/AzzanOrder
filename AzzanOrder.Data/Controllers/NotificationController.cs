using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace AzzanOrder.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public NotificationController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Notification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
          if (_context.Notifications == null)
          {
              return NotFound();
          }
            return await _context.Notifications.ToListAsync();
        }

        // GET: api/Notification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
          if (_context.Notifications == null)
          {
              return NotFound();
          }
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }
            return notification;
        }

        [HttpGet("Member/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationByMember(int id)
        {
            if (_context.Notifications == null)
            {
                return NotFound();
            }
            var notification = await _context.Notifications.Where(x => x.MemberId == id).ToListAsync();

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        [HttpGet("Employee/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationByEmployee(int id)
        {
            if (_context.Notifications == null)
            {
                return NotFound();
            }
            var notification = await _context.Notifications.Where(x => x.EmployeeId == id).ToListAsync();

            if (notification == null)
            {
                return NotFound();
            }
            return notification;
        }

        [HttpGet("GetByOwnerId/{id}")]
		public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationByOwner(int id)
		{
			if (_context.Notifications == null)
			{
				return NotFound();
			}
			var notification = await _context.Notifications.Where(x => x.EmployeeId == id).ToListAsync();

			if (notification == null)
			{
				return NotFound();
			}

			return notification;
		}

		// PUT: api/Notification/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("Update")]
        public async Task<IActionResult> PutNotification(int? id, Notification notification)
        {
            if(id != null)
            {
				if (_context.Notifications.Where(n => n.EmployeeId == id && n.Title.Equals("Renew your subscription")).IsNullOrEmpty())
				{
					var newnotification = new Notification
					{
						EmployeeId = id,
						Content = notification.Content,
						Title = "Renew your subscription",
						IsRead = false,
					};
					_context.Notifications.Add(newnotification);
					await _context.SaveChangesAsync();
					return Ok("Add renew your subscription notification");
				}
				else
				{
					var noti = _context.Notifications.Where(n => n.EmployeeId == id && n.Title.Equals("Renew your subscription")).First();
					noti.Content = notification.Content;
					await _context.SaveChangesAsync();
					return Ok("Update renew your subscription notification");
				}
			}
			
			_context.Entry(notification).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();   
            }

            return Ok(notification);
        }

        // POST: api/Notification
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
          if (_context.Notifications == null)
          {
              return Problem("Entity set 'OrderingAssistSystemContext.Notifications'  is null.");
          }
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(notification);
        }

        // DELETE: api/Notification/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            if (_context.Notifications == null)
            {
                return NotFound();
            }
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        private bool NotificationExists(int id)
        {
            return (_context.Notifications?.Any(e => e.NotificationId == id)).GetValueOrDefault();
        }
    }
}
