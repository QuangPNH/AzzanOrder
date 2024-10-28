using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Models;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
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
            return await _context.Orders.ToListAsync();
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
                //&& x.OrderDate > oneHourAgo
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

                // Add the order to the context
                _context.Orders.Add(order);

                // Save the order to generate the OrderId
                await _context.SaveChangesAsync();

                // Add the OrderId to each order detail and reset OrderDetailId
                foreach (var orderDetail in order.OrderDetails)
                {
                    orderDetail.OrderId = order.OrderId;
                    orderDetail.OrderDetailId = 0; // Ensure the ID is reset
                    _context.OrderDetails.Add(orderDetail);
                }

                // Save the order details
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

        //https://www.vietqr.io/danh-sach-api/link-tao-ma-nhanh/api-tao-ma-qr/
        //https://www.vietqr.io/en/danh-sach-api/link-tao-ma-nhanh/
        [HttpGet("QR/{price}")]
        public async Task<IActionResult> VietQR(int price)
        {
            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                var htmlData = await httpClient.GetStringAsync("https://api.vietqr.io/v2/banks");
                var listBankData = JsonConvert.DeserializeObject<Api.Bank>(htmlData);
            }
            var apiRequest = new Api.ApiRequest
            {
                acqId = 970436, //Vietcombank
                accountNo = 9967375046, //stk
                accountName = "Cusine De La FPT", //ten tai khoan
                amount = 500, //price
                format = "text",
                template = "compact2"
            };
            var jsonRequest = JsonConvert.SerializeObject(apiRequest);
            // use restsharp for request api.
            var restClient = new RestClient("https://api.vietqr.io/v2/generate");
            var request = new RestRequest
            {
                Method = RestSharp.Method.Post
            };
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            var response = await restClient.ExecuteAsync(request);
            var content = response.Content;

            var dataResult = JsonConvert.DeserializeObject<Api.ApiResponse>(content);
            var image = Base64ToImage(dataResult.data.qrDataURL.Replace("data:image/png;base64,", ""));
            base64Image = image;

            return Ok(new { base64Image });
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

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}

