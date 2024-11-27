using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;
using Newtonsoft.Json;
using RestSharp;

namespace AzzanOrder.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;
        public string base64Image { get; set; }

        public OrderController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            return await _context.Orders.Include(o => o.Table).Include(o => o.Member).Include(o => o.OrderDetails).ThenInclude(od => od.MenuItem).ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("GetCustomerOrder/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetGetCustomerOrder(int id, int?  employeeId)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var orders = await _context.Orders.Include(o => o.OrderDetails)
                    .ThenInclude(od => od.MenuItem).Where(
                x => x.MemberId == id  &&
                x.OrderDate > DateTime.Now.AddHours(-1)
                ).ToListAsync();

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        [HttpGet("GetOrderByTableQr/{qr}/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByTableQr(string qr, int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            var oneHourAgo = DateTime.Now.AddHours(-1);
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.MenuItem)
                .Include(o => o.Table)
                .Where(x => x.Table.Qr == qr
                            && x.Table.EmployeeId == id
                            && x.OrderDate > oneHourAgo
                ).ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound();
            }

            return orders;
        }


        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                if (_context.Orders == null)
                {
                    return Problem("Entity set 'OrderingAssistSystemContext.Orders' is null.");
                }

                if (order.OrderDetails == null || !order.OrderDetails.Any())
                {
                    return BadRequest("Order must contain at least one order detail.");
                }
                order.OrderDate = DateTime.Now;
                // Add the order to the context
                _context.Orders.Add(order);

                // Save the order to generate the OrderId
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            foreach (var orderDetail in order.OrderDetails)
            {
                _context.OrderDetails.Remove(orderDetail);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("PendingOrder")]
        public async Task<ActionResult<IEnumerable<Order>>> GetPendingOrder()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.MenuItem)
                .Include(o => o.Table)
                .Where(x => x.Status == false
                            //&& x.OrderDate > DateTime.Now.AddHours(-1)
                ).ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound();
            }

            return orders;
        }

        private string Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                string base64Image = Convert.ToBase64String(ms.ToArray());
                ms.Write(imageBytes, 0, imageBytes.Length);
                return base64Image;
            }
        }

        private string ImageToBase64(string imagePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }

    }
}

