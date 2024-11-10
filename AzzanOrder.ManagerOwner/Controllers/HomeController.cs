using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _apiUrl = "https://localhost:7183/api/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> IndexAsync()
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);

            if ((await authorizeLogin.CheckLogin()).Equals("owner"))
            {
                return View();
            }
            {
                return RedirectToAction("Login", "Home");
            }
        }

        //00867f56a5a886a975463d3ec7941061
        public IActionResult Privacy()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Login()
        {
            HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson);
            Console.WriteLine("sfdjhsbfjs " + empJson);
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);

            Console.WriteLine("ssdsdjjsc " + await authorizeLogin.CheckLogin());
            if ((await authorizeLogin.CheckLogin()).Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if ((await authorizeLogin.CheckLogin()).Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginAction(Employee employee)
        {
            Employee emp = null;
            Owner owner = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/Manager/Phone/" + employee.Phone);
                    if (res.IsSuccessStatusCode)
                    {
                        string data = await res.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Employee>(data);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                catch (HttpRequestException e)
                {
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }
            }

            if (emp == null)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage res = await client.GetAsync(_apiUrl + "Owner/Phone/" + employee.Phone);
                        if (res.IsSuccessStatusCode)
                        {
                            string data = await res.Content.ReadAsStringAsync();
                            owner = JsonConvert.DeserializeObject<Owner>(data);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                    }
                }
                if (owner == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    string otp = new Random().Next(000000, 999999).ToString();

                    otp = "123456"; // For testing only, remove this line in production

                    HttpContext.Session.SetString("OTP", otp);

                    /*// Send OTP via Twilio
					var accountSid = "ACd5083d30edb839433981a766a0c2e2fd";
					var authToken = "";
					TwilioClient.Init(accountSid, authToken);
					var messageOptions = new CreateMessageOptions(new PhoneNumber("+84388536414"))
					{
						From = new PhoneNumber("+19096555985"),
						Body = "Your OTP is " + otp
					};
					MessageResource.Create(messageOptions);*/

                    // Redirect to the OTP input page
                    var model = new AzzanOrder.ManagerOwner.Models.Model { owner = owner, employee = emp };

                    var ownerJson = JsonConvert.SerializeObject(owner);
                    HttpContext.Response.Cookies.Append("LoginInfo", ownerJson, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30)
                    });

                    return View("OTPInput", model);

                }
            }
            else
            {
                string otp = new Random().Next(000000, 999999).ToString();

                otp = "123456"; // For testing only, remove this line in production

                HttpContext.Session.SetString("OTP", otp);

                /*// Send OTP via Twilio
                var accountSid = "ACd5083d30edb839433981a766a0c2e2fd";
                var authToken = "";
                TwilioClient.Init(accountSid, authToken);
                var messageOptions = new CreateMessageOptions(new PhoneNumber("+84388536414"))
                {
                    From = new PhoneNumber("+19096555985"),
                    Body = "Your OTP is " + otp
                };
                MessageResource.Create(messageOptions);*/

                // Redirect to the OTP input page
                var model = new AzzanOrder.ManagerOwner.Models.Model { employee = emp };

                var empJson = JsonConvert.SerializeObject(emp);
                HttpContext.Response.Cookies.Append("LoginInfo", empJson, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30)
                });

                return View("OTPInput", model);
            }
        }

        [HttpPost]
        public IActionResult VerifyOtp(string otp)
        {
            var sessionOtp = HttpContext.Session.GetString("OTP");
            // Check if the OTP matches
            if (otp == sessionOtp?.ToString())
            {
                TempData.Remove("OTP");
                try
                {
                    HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string loginInfoJson);
                    var loginInfo = JsonConvert.DeserializeObject<Employee>(loginInfoJson);
                    if (loginInfo.RoleId != null)
                    {
                        return RedirectToAction("List", "Employee");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch
                {

                }
                return View("OTPInput");
            }
            else
            {
                var model = new AzzanOrder.ManagerOwner.Models.Model();
                ModelState.AddModelError(string.Empty, "Invalid OTP. Please try again.");
                return View("OTPInput", model);
            }
        }


        [HttpPost]
        public async Task<IActionResult> LogoutAction()
        {
            HttpContext.Response.Cookies.Delete("LoginInfo");
            return RedirectToAction("Login", "Home");
        }



        public async Task<IActionResult> Subscribe()
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if ((await authorizeLogin.CheckLogin()).Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }

            Api.Bank theBank = new Api.Bank();
            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                var htmlData = await httpClient.GetStringAsync("https://api.vietqr.io/v2/banks");
                theBank = JsonConvert.DeserializeObject<Api.Bank>(htmlData);
            }

            var model = new AzzanOrder.ManagerOwner.Models.Model { bankDatums = theBank.data };
            return View(model);
        }

        [HttpPost]
        public IActionResult SubscribeAction(string pack, Model model)
        {
            // Implement necessary subscription logic here
            // Example processing logic based on the pack type
            string redirectUrl = "https://localhost:3002/?";

            Bank bank = model.bank;

            if (model.owner != null)
            {
                // Prepare the query parameters with Owner details
                var ownerParams = new Dictionary<string, string>
            {
            { "Price", "0" }, // Adjust price based on pack
            { "Item", "Subscribe" + char.ToUpper(pack[0]) + pack.Substring(1) + "Pack" },      // e.g., SubscribeMonthlyPack
            { "Message", char.ToUpper(pack[0]) + pack.Substring(1) + "Pack" },
            { "OwnerName", model.owner.OwnerName ?? string.Empty },
            { "Gender", model.owner.Gender.HasValue ? (model.owner.Gender.Value ? "Male" : "Female") : string.Empty },
            { "Phone", model.owner.Phone ?? string.Empty },
            { "Gmail", model.owner.Gmail ?? string.Empty },
            { "BirthDate", model.owner.BirthDate.HasValue ? model.owner.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty },
            { "BankId", model.owner.BankId?.ToString() ?? string.Empty },
            { "Image", model.owner.Image ?? string.Empty },
            { "IsDelete", model.owner.IsDelete.HasValue ? model.owner.IsDelete.Value.ToString() : string.Empty },
            { "SubscriptionStartDate", model.owner.SubscriptionStartDate.ToString("yyyy-MM-dd") },
            { "SubscribeEndDate", model.owner.SubscribeEndDate.ToString("yyyy-MM-dd") },

            // Bank details
            { "PAYOS_CLIENT_ID", model.owner.Bank.PAYOS_CLIENT_ID ?? string.Empty },
            { "PAYOS_API_KEY", model.owner.Bank.PAYOS_API_KEY ?? string.Empty },
            { "PAYOS_CHECKSUM_KEY", model.owner.Bank.PAYOS_CHECKSUM_KEY ?? string.Empty }
        };

                // Construct the URL with the query parameters
                redirectUrl += string.Join("&", ownerParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            }
            else
            {
                // Handle case when model.Owner is null if necessary
                return RedirectToAction("Error");  // Redirect to an error page or display a message if Owner is missing
            }

            return Redirect(redirectUrl);  // Redirect to the dynamically generated URL
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


