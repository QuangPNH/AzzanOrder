namespace NetCoreDemo.Controllers;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

public class CheckoutController : Controller
{
	private readonly PayOS _payOS;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public CheckoutController(PayOS payOS, IHttpContextAccessor httpContextAccessor)
	{
		_payOS = payOS;
		_httpContextAccessor = httpContextAccessor;
	}


    //https://localhost:3002/?tableqr=QR_002/1&Price=1000.00&Item=OASItem&Message=Order
    //https://localhost:3002/?Price=30000&Item=SubscribeMontlyPack&Message=MonthyPack
    //http://localhost:3002/?Price=30000&Item=SubscribeMonthlyPack&Message=MonthlyPack&OwnerName=&OwnerEmail=
    [HttpGet("/")]
    public async Task<IActionResult> IndexAsync([FromQuery] string? tableqr, [FromQuery] string? Item, [FromQuery] double? Price, [FromQuery] string? Message)
    {
        Console.WriteLine("tableqr: " + tableqr);
        Console.WriteLine("Item: " + Item);
        Console.WriteLine("Price: " + Price);
        Console.WriteLine("Message: " + Message);
        // Save 'tableqr' and 'Item' to cookies
        if (!string.IsNullOrEmpty(tableqr))
        {
            Response.Cookies.Append("tableqrPayOs", tableqr, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });
        }
        if (!string.IsNullOrEmpty(Item))
        {
            Response.Cookies.Append("ItemType", Item, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });
        }
        try
        {
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            ItemData item = new ItemData(Item, 1, 1000);
            List<ItemData> items = new List<ItemData> { item };
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            PaymentData paymentData = new PaymentData(
                orderCode,
                (int)Price,
                Message + orderCode,
                items,
                $"{baseUrl}/cancel",
                $"{baseUrl}/success"
            );

            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

            return Redirect(createPayment.checkoutUrl);
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception);
            //return Redirect("/");
            return View();
        }
    }



    [HttpGet("/cancel")]
    public IActionResult Cancel()
    {
        HttpContext.Request.Cookies.TryGetValue("tableqrPayOs", out string tableqr);
        HttpContext.Request.Cookies.TryGetValue("ItemType", out string itemType);

        if (itemType.Contains("Subscribe"))
        {
            return Redirect("https://localhost:7093/Home/Subscribe");
        }

        //return Redirect("http://localhost:5173/?tableqr=" + tableqr + "&status=cancel");
        return Redirect("http://localhost:5173/?tableqr=" + tableqr + "&status=success");
    }


    [HttpGet("/success")]
    public async Task<IActionResult> Success([FromQuery] int memberId)
    {
        HttpContext.Request.Cookies.TryGetValue("tableqrPayOs", out string tableqr);
        HttpContext.Request.Cookies.TryGetValue("ItemType", out string itemType);

        if (itemType.Contains("Subscribe"))
        {
            return Redirect("https://localhost:7093/Home/Index");
        }
        return Redirect("http://localhost:5173/?tableqr=" + tableqr + "&status=success");
    }


    //var _apiUrl = $"https://localhost:7183/api/Member/";
    //      using (HttpClient client = new HttpClient())
    //      {
    //          try
    //          {
    //              HttpResponseMessage res = await client.GetAsync(_apiUrl + memberId);
    //              if (res.IsSuccessStatusCode)
    //              {
    //                  string data = await res.Content.ReadAsStringAsync();
    //                  roles = JsonConvert.DeserializeObject<List<Role>>(data);
    //              }
    //              else
    //              {
    //                  // Handle the error response here
    //                  ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
    //              }
    //          }
    //          catch (HttpRequestException e)	
    //          {
    //              // Handle the exception here
    //              ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
    //          }
    //      }
}
