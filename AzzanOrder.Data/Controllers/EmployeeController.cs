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
            return await _context.Employees.ToListAsync();
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
          var e = new Employee { EmployeeName = employee.EmployeeName, Gender = employee.Gender, Phone = employee.Phone, Gmail = employee.Gmail, BirthDate = employee.BirthDate, RoleId = employee.RoleId, HomeAddress = employee.HomeAddress, WorkAddress = employee.WorkAddress, Image = employee.Image};
            _context.Employees.Add(e);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = e.EmployeeId }, e);
        }

        // DELETE: api/Employee/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
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
            var mi = _context.MenuItems.FirstOrDefault(mi => mi.EmployeeId == employee.EmployeeId);
            var p = _context.Promotions.FirstOrDefault(p => p.EmployeeId == employee.EmployeeId);
            var n = _context.MenuItems.FirstOrDefault(n => n.EmployeeId == employee.EmployeeId);
            if(n != null || mi != null || p != null) 
            {
                return BadRequest("Cant't be detele this cause some data had use this information employee's");
            }
            _context.Employees.Remove(employee);
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