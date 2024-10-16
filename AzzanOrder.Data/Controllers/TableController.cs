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
    public class TableController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public TableController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Table
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetTables()
        {
          if (_context.Tables == null)
          {
              return NotFound();
          }
            return await _context.Tables.ToListAsync();
        }

        // GET: api/Table/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
          if (_context.Tables == null)
          {
              return NotFound();
          }
            var table = await _context.Tables.FindAsync(id);

            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }

        // PUT: api/Table/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutTable([Bind("TableId", "Qr", "Status", "EmployeeId")]Table table)
        {
            if (!TableExists(table.TableId))
            {
                return NotFound("This table not exist");
            }
            _context.Entry(table).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableExists(table.TableId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Tables.FirstOrDefault(t => t.TableId == table.TableId));
        }

        // POST: api/Table
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Table>> PostTable(Table table)
        {
          if (_context.Tables == null)
          {
              return Problem("List table are null.");
          }
          var tab = new Table() { Qr = table.Qr , Orders = table.Orders, Status = table.Status, EmployeeId = table.EmployeeId};
            _context.Tables.Add(tab);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTable", new { id = tab.TableId }, tab);
        }

        // DELETE: api/Table/5
        [HttpDelete("Detele")]
        public async Task<IActionResult> DeleteTable(Table table)
        {
            if (_context.Tables == null)
            {
                return NotFound();
            }
            var t = await _context.Tables.FirstOrDefaultAsync(t => t.TableId == table.TableId);
            if (t == null)
            {
                return NotFound("Table not exist");
            }

            _context.Tables.Remove(t);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        private bool TableExists(int id)
        {
            return (_context.Tables?.Any(e => e.TableId == id)).GetValueOrDefault();
        }
    }
}
