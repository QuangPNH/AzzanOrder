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
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderingAssistSystemContext _context;

        public OrderDetailController(OrderingAssistSystemContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
          if (_context.OrderDetails == null)
          {
              return NotFound();
          }
            return await _context.OrderDetails.Include(od => od.MenuItem).ToListAsync();
        }


        [HttpGet("Employee/{id}")]
		public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetailsByEmployee(int id)
		{
			if (_context.OrderDetails == null)
			{
				return NotFound();
			}

			var orderDetails = await _context.OrderDetails
				.Include(od => od.MenuItem)
				.Include(od => od.Order)
					.ThenInclude(o => o.Table)
				.Include(od => od.Order)
					.ThenInclude(o => o.Member)
				.Where(od => od.Order != null && od.Order.Table != null && od.Order.Table.EmployeeId == id)
				.Select(od => new OrderDetail
				{
					OrderDetailId = od.OrderDetailId,
					OrderId = od.OrderId,
					Quantity = od.Quantity,
					MenuItemId = od.MenuItemId,
					Status = od.Status,
					Description = od.Description,
					MenuItem = new MenuItem
					{
						MenuItemId = od.MenuItem.MenuItemId,
						ItemName = od.MenuItem.ItemName,
						Price = od.MenuItem.Price,
						// Exclude OrderDetails to avoid circular reference
					},
					Order = new Order
					{
						OrderId = od.Order.OrderId,
						TableId = od.Order.TableId,
						MemberId = od.Order.MemberId,
						OrderDate = od.Order.OrderDate,
						Cost = od.Order.Cost,
						Tax = od.Order.Tax,
						Status = od.Order.Status,
						Table = od.Order.Table,
						Member = od.Order.Member != null ? new Member
						{
							MemberId = od.Order.Member.MemberId,
							MemberName = od.Order.Member.MemberName,
							Gender = od.Order.Member.Gender,
							Phone = od.Order.Member.Phone,
							Gmail = od.Order.Member.Gmail,
							BirthDate = od.Order.Member.BirthDate,
							Address = od.Order.Member.Address,
							Point = od.Order.Member.Point,
							Image = "",
							IsDelete = od.Order.Member.IsDelete,
							Feedbacks = new List<Feedback>(),
							MemberVouchers = new List<MemberVoucher>(),
							Notifications = new List<Notification>(),
							Orders = new List<Order>()
						} : null,
						MemberVouchers = new List<MemberVoucher>(),
					}
				})
				.ToListAsync();

			return orderDetails;
		}


        // GET: api/OrderDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
          if (_context.OrderDetails == null)
          {
              return NotFound();
          }
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        // PUT: api/OrderDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
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

        // POST: api/OrderDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("")]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
          if (_context.OrderDetails == null)
          {
              return Problem("Entity set 'OrderingAssistSystemContext.OrderDetails'  is null.");
          }
           if(_context.Orders.Find(orderDetail.OrderId) == null && _context.MenuItems.Find(orderDetail.MenuItemId) != null)
            {

            }
            _context.OrderDetails.Add(orderDetail);

            await _context.SaveChangesAsync();

            return Ok(orderDetail);
        }

        // DELETE: api/OrderDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}
