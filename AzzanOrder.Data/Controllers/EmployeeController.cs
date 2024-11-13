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
    public class EmployeeController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public EmployeeController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            return await _context.Employees.Include(e => e.Role).ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // GET: api/Employee/5
        [HttpGet("Phone/{phone}")]
        public async Task<ActionResult<Employee>> GetEmployeeByPhone(string phone)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = _context.Employees.Include(e => e.Role).FirstOrDefault(e => e.Phone.Equals(phone));

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }


        // GET: api/Employee/5
        [HttpGet("Staff/Phone/{phone}")]
        public async Task<ActionResult<Employee>> GetStaffByPhone(string phone)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = _context.Employees.Include(e => e.Role).Where(e => e.Role.RoleName.ToLower().Equals("staff") || e.Role.RoleName.ToLower().Equals("bartender")).FirstOrDefault(e => e.Phone.Equals(phone));
            Console.WriteLine("djhfjshd "+phone);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }


        // GET: api/Employee/5
        [HttpGet("Manager/Phone/{phone}")]
        public async Task<ActionResult<Employee>> GetManagerByPhone(string phone)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = _context.Employees.Include(e => e.Role).Include(e => e.Owner).FirstOrDefault(e => e.Phone.Equals(phone) && e.Role.RoleName.ToLower().Equals("manager"));

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutEmployee(Employee employee)
        {
            if (!EmployeeExists(employee.EmployeeId))
            {
                return NotFound("Employee not exist");
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.EmployeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Update success");
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employees == null)
          {
              return Problem("Entity set 'OrderingAssistSystemContext.Employees'  is null.");
          }

            if (!_context.Roles.Any() && employee.Role != null)
            {
                _context.Roles.Add(employee.Role);
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            //List<Promotion> promotions = await _context.Promotions.Where(p => p.EmployeeId == id).ToListAsync();
            //foreach (var promotion in promotions)
            //{
            //    _context.Promotions.Remove(promotion);
            //}

            //List<Notification> notifications = await _context.Notifications.Where(n => n.EmployeeId == id).ToListAsync();
            //foreach (var notification in notifications)
            //{
            //    _context.Notifications.Remove(notification);
            //}

            //List<MenuItem> menuItems = await _context.MenuItems.Where(m => m.EmployeeId == id).ToListAsync();
            //foreach (var menuItem in menuItems)
            //{
            //    _context.MenuItems.Remove(menuItem);
            //}

            //foreach (MenuItem menuItem in menuItems)
            //{
            //    _context.MenuCategories.RemoveRange(await _context.MenuCategories.Where(mc => mc.MenuItemId == menuItem.MenuItemId).ToListAsync());
            //}

            //foreach(MenuItem menuItem in menuItems)
            //{
            //    _context.OrderDetails.RemoveRange(await _context.OrderDetails.Where(od => od.MenuItemId == menuItem.MenuItemId).ToListAsync());
            //}

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.IsDelete = true;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}

//Tuan cx da o day