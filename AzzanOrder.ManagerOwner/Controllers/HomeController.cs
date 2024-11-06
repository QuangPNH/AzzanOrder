using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Numerics;
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

				TempData["OTP"] = otp;

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
				return View("OTPInput", model);
			}
            else
            {
				Console.WriteLine("Nooooo");
				return RedirectToAction("Login", "Home");
            }
		}

		[HttpPost]
		public IActionResult VerifyOtp(string otp)
		{
			// Check if the OTP matches
			if (otp == TempData["OTP"]?.ToString())
			{
				TempData.Remove("OTP"); // Clear OTP after use
				return RedirectToAction("List", "Employee");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid OTP. Please try again.");
				return View("OTPInput"); // Return to OTP view if OTP is invalid
			}
		}


		[HttpPost]
        public async Task<IActionResult> LogoutAction()
        {
            HttpContext.Response.Cookies.Delete("LoginInfo");
            return RedirectToAction("Login", "Home");
        }



        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (await CheckEmployeeCookie())
            {
                return RedirectToAction("List", "Employee");
            }

            return View();
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



        private async Task<bool> CheckEmployeeCookie()
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


