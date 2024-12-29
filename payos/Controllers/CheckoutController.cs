namespace NetCoreDemo.Controllers;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using payos.Models;
using NetCoreDemo.Types;
using System.Text.Json;

public class CheckoutController : Controller
{
    private PayOS _payOS;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AzzanOrder.ManagerOwner.Models.Config _config = new AzzanOrder.ManagerOwner.Models.Config();


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

        if (!string.IsNullOrEmpty(tableqr))
        {
            Response.Cookies.Append("tableqrPayOs", tableqr, new CookieOptions
            {
                //HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });
        }

        if (!string.IsNullOrEmpty(Item))
        {
            Response.Cookies.Append("ItemType", Item, new CookieOptions
            {
                //HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });
        }

        try
        {
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            List<ItemData> items = new List<ItemData>();
            int total = 0;
            var a = System.Text.Json.JsonSerializer.Deserialize<List<Item>>(Item);
            foreach (var i in a)
            {
                items.Add(new ItemData(i.name, i.quantity, (i.price - i.discount) * i.quantity));
                total += i.price * i.quantity;
            }

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
        [FromQuery] int? Price,
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
        }*/



        Response.Cookies.Append("log1", "ye", new CookieOptions
        {
            //HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        });



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
            // Generate a unique order code
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));

            // Create the item list with the provided item data
            List<ItemData> items = new List<ItemData>();

            items.Add(new ItemData(Item, 1, (int)Price));

            // Get the base URL of the current request
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            // Set up the payment data, including the additional owner information in the message if needed
            PaymentData paymentData = new PaymentData(
                orderCode,
                (int)Price,
                Message + orderCode,
                items,
                $"{baseUrl}/success/?Item=" + Item,
                $"{baseUrl}/cancel/?Item=" + Item
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
    public async Task<IActionResult> Success([FromQuery] string? Item, [FromQuery] int memberId)
    {
        Console.WriteLine("Success running yeh");
        HttpContext.Request.Cookies.TryGetValue("tableqrPayOs", out string tableqr);
        HttpContext.Request.Cookies.TryGetValue("ItemType", out string itemType);

        if ((itemType != null && itemType.Contains("Subscribe")) || (!string.IsNullOrEmpty(Item) && Item.Contains("Subscribe")))
        {
            Response.Cookies.Delete("ItemType");
            Console.WriteLine("When succ" + HttpContext.Request.Cookies.TryGetValue("OwnerData", out string ownerJson));
            Console.WriteLine("Tets cookie " + ownerJson);
            Console.WriteLine("Tets cookie " + Request.Cookies["OwnerData"]);

            Owner owner = JsonConvert.DeserializeObject<Owner>(ownerJson);

            using (HttpClient client = new HttpClient())
            {
                // Check if the phone number exists
                HttpResponseMessage getResponse = await client.GetAsync($"{_config._apiUrl}Owner/Phone/{owner.Phone}");
                if (getResponse.IsSuccessStatusCode)
                {
                    string addMess = await getResponse.Content.ReadAsStringAsync();
                    var o = JsonConvert.DeserializeObject<Owner>(addMess);
                    owner.Image = o.Image;
                    // Phone number exists, update subscription dates

                    using (HttpResponseMessage addResponse = await client.PutAsJsonAsync($"{_config._apiUrl}Owner/Update/", owner))
                    {
                        if (addResponse.IsSuccessStatusCode)
                        {
                            string addMessage = await addResponse.Content.ReadAsStringAsync();
                            Console.WriteLine(addMessage);
                        }
                    }
                }
                else
                {
                    owner.IsFreeTrial = true;
                    // Phone number does not exist, add new owner
                    using (HttpResponseMessage getRes = await client.PostAsJsonAsync($"{_config._apiUrl}Bank/Add", owner.Bank))
                    {
                        if (getRes.IsSuccessStatusCode)
                        {
                            string mes = await getRes.Content.ReadAsStringAsync();
                            var bank = JsonConvert.DeserializeObject<Bank>(mes);
                            owner.BankId = bank.BankId;
                        }
                    }
                    HttpResponseMessage addResponse = await client.PostAsJsonAsync($"{_config._apiUrl}Owner/Add/", owner);
                    if (addResponse.IsSuccessStatusCode)
                    {
                        string addMessage = await addResponse.Content.ReadAsStringAsync();
                        Console.WriteLine(addMessage);
                    }
                }

                HttpResponseMessage registerResponse = await client.PostAsJsonAsync($"{_config._manager}Home/OwnerRegister", owner);
                if (registerResponse.IsSuccessStatusCode)
                {
                    string registerMessage = await registerResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(registerMessage);
                }
            }

            HttpContext.Response.Cookies.Append("LoginInfo", ownerJson, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return Redirect($"{_config._manager}Home/Index");
        }
        return Redirect($"{_config._client}?tableqr=" + tableqr + "&status=success");
    }

    [HttpGet("/cancel")]
    public IActionResult Cancel([FromQuery] string? Item)
    {

        HttpContext.Request.Cookies.TryGetValue("tableqrPayOs", out string tableqr);
        HttpContext.Request.Cookies.TryGetValue("ItemType", out string itemType);

        if ((itemType != null && itemType.Contains("Subscribe")) || (!string.IsNullOrEmpty(Item) && Item.Contains("Subscribe")))
        {
            Response.Cookies.Delete("ItemType");
            return Redirect($"{_config._manager}Home/Index");
        }
        else
        {
            return Redirect($"{_config._client}?tableqr=" + tableqr + "&status=cancel");
        }

        //return Redirect("http://localhost:5173/?tableqr=" + tableqr + "&status=cancel");

    }
}
