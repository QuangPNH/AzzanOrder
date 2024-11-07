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

	//https://localhost:3002/?tableqr=QR_002/1&Price=1000&Item=OASItem&Message=Order
	[HttpGet("/")]
	public async Task<IActionResult> IndexAsync([FromQuery] string tableqr,[FromQuery] string Item,[FromQuery] double Price,[FromQuery] string Message)
	{
		Console.WriteLine("sfsdfsf " + tableqr);
		Console.WriteLine("Item: " + Item);
		Console.WriteLine("Price: " + Price);
		Console.WriteLine("Message: " + Message);

		// Save the 'tableqr' value to a cookie
		if (!string.IsNullOrEmpty(tableqr))
		{
			Response.Cookies.Append("tableqrPayOs", tableqr, new CookieOptions
			{
				HttpOnly = true, // Makes the cookie accessible only through HTTP, not JavaScript
				Expires = DateTimeOffset.UtcNow.AddDays(1) // Set the cookie expiration time
			});
		}

		try
		{
			int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
			ItemData item = new ItemData(Item, 1, 1000);
			List<ItemData> items = new List<ItemData> { item };

			// Get the current request's base URL
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
			return Redirect("/");
		}
	}

	[HttpGet("/cancel")]
	public IActionResult Cancel()
	{
		//return Redirect("http://localhost:5173/");
		HttpContext.Request.Cookies.TryGetValue("tableqrPayOs", out string tableqr);
		return Redirect("http://localhost:5173/?tableqr=" + tableqr + "&status=success");
	}

	[HttpGet("/success")]
	public async Task<ActionResult> Success([FromQuery] int memberId)
	{
		HttpContext.Request.Cookies.TryGetValue("tableqrPayOs", out string tableqr);
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
        return Redirect("http://localhost:5173/?tableqr=" + tableqr + "&status=success");
	}
}
