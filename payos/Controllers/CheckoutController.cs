namespace NetCoreDemo.Controllers;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using payos.Models;
using NetCoreDemo.Types;

public class CheckoutController : Controller
{
    private PayOS _payOS;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckoutController(PayOS payOS, IHttpContextAccessor httpContextAccessor)
    {
        _payOS = payOS;
        _httpContextAccessor = httpContextAccessor;
    }

    //https://localhost:3002/?tableqr=QR_002/1&Price=1000.00&Item=OASItem&Message=Order
    //https://localhost:3002/?Price=30000&Item=SubscribeMontlyPack&Message=MonthyPack
    //http://localhost:3002/?Price=30000&Item=SubscribeMonthlyPack&Message=MonthlyPack&OwnerName=&OwnerEmail=
    //Normal payment
    [HttpPost("/")]
    public async Task<string> Index([FromBody] Bank bank, [FromQuery] string tableqr, [FromQuery] string Item, [FromQuery] double Price, [FromQuery] string Message)
    {
        _payOS = new PayOS(bank.PAYOS_CLIENT_ID ?? "", bank.PAYOS_API_KEY ?? "", bank.PAYOS_CHECKSUM_KEY ?? "");
        Console.WriteLine("PAYOS_CLIENT_ID: " + bank.PAYOS_CLIENT_ID);
        if (_payOS == null)
        {
            return "Payment credentials not set.";
        }

        Console.WriteLine("tableqr: " + tableqr);
        Console.WriteLine("Item: " + Item);
        Console.WriteLine("Price: " + Price);
        Console.WriteLine("Message: " + Message);

        if (!string.IsNullOrEmpty(tableqr))
        {
            Response.Cookies.Append("tableqrPayOs", tableqr, new CookieOptions
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

            //return Redirect(createPayment.checkoutUrl);
            return createPayment.checkoutUrl;
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception);
            return exception.ToString();
        }
    }





    //Subscription payment
    [HttpPost("/Subscribe")]
    public async Task<string> Subscribe(
        [FromBody] Owner owner,
        [FromQuery] string? Item,
        [FromQuery] double? Price,
        [FromQuery] string? Message)
    {
        string ownerJson = JsonConvert.SerializeObject(owner);
        Console.WriteLine("Owner cookeie " + ownerJson);

        /*if (!string.IsNullOrEmpty(ownerJson))
        {
            Response.Cookies.Append("OwnerData", ownerJson, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });
            Console.WriteLine(" coOwnersergokeie " + HttpContext.Request.Cookies.TryGetValue("OwnerData", out var ownerData));
        }
        if (!string.IsNullOrEmpty(Item))
        {
            Response.Cookies.Append("ItemType", Item, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });
        }*/

        try
        {
            // Generate a unique order code
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));

            // Create the item list with the provided item data
            ItemData item = new ItemData(Item, 1, 1000);
            List<ItemData> items = new List<ItemData> { item };

            // Get the base URL of the current request
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            // Set up the payment data, including the additional owner information in the message if needed
            PaymentData paymentData = new PaymentData(
                orderCode,
                (int)Price.GetValueOrDefault(),
                Message + orderCode,
                items,
                $"{baseUrl}/success",
                $"{baseUrl}/cancel"
            );

            // Create the payment link
            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

            //return Redirect(createPayment.checkoutUrl);
            return createPayment.checkoutUrl;
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception);
            return exception.ToString();
        }
    }

    [HttpGet("/success")]
    public async Task<IActionResult> Success([FromQuery] int memberId)
    {
        Console.WriteLine("Success running yeh");
        HttpContext.Request.Cookies.TryGetValue("tableqrPayOs", out string tableqr);
        HttpContext.Request.Cookies.TryGetValue("ItemType", out string itemType);

        if (itemType.Contains("Subscribe"))
        {
            Console.WriteLine("When succ" + HttpContext.Request.Cookies.TryGetValue("OwnerData", out string ownerJson));
            Console.WriteLine("Tets cookie " + ownerJson);
            Console.WriteLine("Tets cookie " + Request.Cookies["OwnerData"]);




            Owner owner = JsonConvert.DeserializeObject<Owner>(ownerJson);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync("https://localhost:7183/api/Owner/Add/", owner))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                    }
                }

                using (HttpResponseMessage res = await client.PostAsJsonAsync("https://localhost:7093/OwnerRegister", owner))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                        Console.WriteLine(message);
                    }
                }
            }

            HttpContext.Response.Cookies.Append("LoginInfo", ownerJson, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return Redirect("https://localhost:7093/Home/Index");
        }
        return Redirect("http://localhost:5173/?tableqr=" + tableqr + "&status=success");
    }



    [HttpGet("/cancel")]
    public IActionResult Cancel()
    {
        Console.WriteLine("Cancel nah");
        HttpContext.Request.Cookies.TryGetValue("tableqrPayOs", out string tableqr);
        HttpContext.Request.Cookies.TryGetValue("ItemType", out string itemType);

        if (itemType.Contains("Subscribe"))
        {
            return Redirect("https://localhost:7093/Home/Subscribe");
        }

        //return Redirect("http://localhost:5173/?tableqr=" + tableqr + "&status=cancel");
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
