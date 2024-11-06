using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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


        public IActionResult Index()
        {
            return View();
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
			if (await CheckLogin())
			{
				return RedirectToAction("List", "Employee");
			}
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> LoginAction(Employee employee)
		{
            bool isManager;
			Employee emp = null;
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

            if (emp != null)
            {

                string otp = new Random().Next(100000, 999999).ToString();

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
            else
            {
				return RedirectToAction("Login", "Home");
            }
		}

		[HttpPost]
		public IActionResult VerifyOtp(string otp)
		{
			var sessionOtp = HttpContext.Session.GetString("OTP");
			// Check if the OTP matches
			if (otp == sessionOtp?.ToString())
			{
				TempData.Remove("OTP"); // Clear OTP after use
				return RedirectToAction("List", "Employee");
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




        public IActionResult Subscribe()
        {
            // Handle the subscribe logic here
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private async Task<bool> CheckLogin()
        {
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/Phone/" + emp.Phone);
                        if (res.IsSuccessStatusCode)
                        {
                            string data = await res.Content.ReadAsStringAsync();
                            emp = JsonConvert.DeserializeObject<Employee>(data);
                        }
                        else
                        {
                            // Handle the error response here
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        // Handle the exception here
                        ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                    }
                }
            }
            if (emp.Phone != null)
            {
                return true;
            }
            return false;
        }
    }
}


