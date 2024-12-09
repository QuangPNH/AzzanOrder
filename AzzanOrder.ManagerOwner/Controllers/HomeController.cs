using AzzanOrder.ManagerOwner.DTOs;
using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using AzzanOrder.ManagerOwner.Services;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _apiUrl = new Config()._apiUrl;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Default()
        {
            return View();
        }
        public async Task<IActionResult> IndexAsync()
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);

            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {

            }
            else if (loginStatus.Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
            }
            else if (loginStatus.Equals("owner expired"))
            {
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            int numberOfOrder = 0;
            int numberOfOrderLastMonth = 0;
            int numberOfEmployee = 0;
            int numberOFManger = 0;
            List<MenuItemSalesDTO> trendingItems = new List<MenuItemSalesDTO>();
            List<MenuItemSalesDTO> failingItems = new List<MenuItemSalesDTO>();
            Owner emp = new Owner();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Owner>(empJson);
            }
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = DateTime.Now;
            var menuItemSales = await FetchSalesData(emp.OwnerId, startDate, endDate);
            if (menuItemSales != null)
            {
                numberOfOrder = menuItemSales.Sum(item => item.Sales);
                trendingItems = menuItemSales.Take(10).ToList();
                if (menuItemSales.Where(mis => mis.Sales == 0).Count() >= 10)
                    failingItems = menuItemSales.Where(mis => mis.Sales == 0).ToList();
                else
                    failingItems = menuItemSales.OrderBy(mis => mis.Sales).Take(10).ToList();
            }
            startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
            var menuItemSalesLastMonth = await FetchSalesData(emp.OwnerId, startDate, endDate);
            if (menuItemSalesLastMonth != null)
            {
                numberOfOrderLastMonth = menuItemSalesLastMonth.Sum(item => item.Sales);
            }
            var employees = await FetchEmpoyeeData(emp.OwnerId);
            numberOfEmployee = employees.Where(e => e.RoleId != 1).Count();
            numberOFManger = employees.Where(e => e.RoleId == 1).Count();
            double percentileChangeInNumberOfOrder = numberOfOrderLastMonth != 0
                ? ((double)(numberOfOrder - numberOfOrderLastMonth) / numberOfOrderLastMonth) * 100
                : 0;

            //var monthlySales = await FetchMonthlySalesData(emp.OwnerId);

            var model = new Dashbroad
            {
                numberOfOrder = numberOfOrder,
                PercentileChangeInNumberOfOrder = percentileChangeInNumberOfOrder,
                numberOfEmployee = numberOfEmployee,
                numberOFManger = numberOFManger,
                trendingItems = trendingItems,
                failingItems = failingItems
            };

            return View(model);
        }

        private async Task<List<MenuItemSalesDTO>> FetchSalesData(int ownerId, DateTime startDate, DateTime endDate)
        {
            var menuItemSales = new List<MenuItemSalesDTO>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = await client.GetAsync($"{_apiUrl}MenuItem/Sales/{ownerId}/{startDate.ToString("yyyy-MM-dd")}/{endDate.ToString("yyyy-MM-dd")}");
                    string data = await res.Content.ReadAsStringAsync();
                    menuItemSales = JsonConvert.DeserializeObject<List<MenuItemSalesDTO>>(data);
                }
                catch (HttpRequestException e)
                {
                    // Handle the exception here
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }
            }

            return menuItemSales;
        }

        private async Task<List<Employee>> FetchEmpoyeeData(int ownerId)
        {
            var emp = new List<Employee>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = await client.GetAsync($"{_apiUrl}Employee");
                    string data = await res.Content.ReadAsStringAsync();
                    emp = JsonConvert.DeserializeObject<List<Employee>>(data);
                }
                catch (HttpRequestException e)
                {
                    // Handle the exception here
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }
            }
            emp = emp.Where(e => e.OwnerId == ownerId && e.IsDelete != true).ToList();
            return emp;
        }

        //00867f56a5a886a975463d3ec7941061
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
            }
            else if (loginStatus.Equals("owner expired"))
            {
                TempData["Message"] = "Your subscription has expired. Please subscribe again.";
            }
            else if (loginStatus.Equals("manager expired"))
            {
                TempData["Message"] = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
            }
            return View();
        }

        //Login method for both owner and manager
        [HttpPost]
        public async Task<IActionResult> Login(Employee employee)
        {
            Employee emp = null;
            Owner owner = null;
            var a = Request.Form["role"].ToString();
            if (a.ToLower().Equals("manager"))
            {
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
					TempData["Message"] = "Login Failed";
                    var model = new Model() { owner = owner, employee = emp };
                    return View(model);
                }
            }
            if (a.ToLower().Equals("owner"))
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
                    TempData["Message"] = "Login Failed";
                    var model = new Model() { owner = owner, employee = emp };
                    return View(model);
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
                    HttpContext.Response.Cookies.Append("TempLoginInfo", ownerJson, new CookieOptions
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
                HttpContext.Response.Cookies.Append("TempLoginInfo", empJson, new CookieOptions
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
                    HttpContext.Request.Cookies.TryGetValue("TempLoginInfo", out string loginInfoJson);
                    HttpContext.Response.Cookies.Append("LoginInfo", loginInfoJson, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30)
                    });
                    HttpContext.Response.Cookies.Delete("TempLoginInfo");

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
            EmployeeService employeeService = new EmployeeService();
            employeeService.CurrentEmployee = null;
            employeeService.CurrentOwner = null;
            return RedirectToAction("Login", "Home");
        }

        public async Task<IActionResult> Subscribe()
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {

            }
            else if (loginStatus.Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
            }
            else if (loginStatus.Equals("owner expired"))
            {

            }
            else if (loginStatus.Equals("manager expired"))
            {

            }

            Api.Bank theBank = new Api.Bank();
            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                var htmlData = await httpClient.GetStringAsync("https://api.vietqr.io/v2/banks");
                theBank = JsonConvert.DeserializeObject<Api.Bank>(htmlData);
            }
            var model = new Model() { bankDatums = theBank.data };
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Subscribe(string pack, Model model)
        {
            if (string.IsNullOrEmpty(model.owner.Bank.PAYOS_CLIENT_ID) || string.IsNullOrEmpty(model.owner.Bank.PAYOS_API_KEY) || string.IsNullOrEmpty(model.owner.Bank.PAYOS_CHECKSUM_KEY) || string.IsNullOrEmpty(model.owner.Phone) || string.IsNullOrEmpty(model.owner.Gmail))
            {
                TempData["Message"] = "Owner PAYOS information, phone number, or Gmail is missing.";
                return RedirectToAction("Subscribe", "Home");
            }

            if (pack.Equals("free"))
            {
                return await SubscribeFreeTrialAction(model);
            }

            string redirectUrl = new Config()._payOS + "Subscribe/?";
            string price = "0";

            bool ownerExist = false;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage getResponse = await client.GetAsync(_apiUrl + $"Owner/Phone/{model.owner.Phone}");
                if (getResponse.IsSuccessStatusCode)
                {
                    var ownerData = await getResponse.Content.ReadAsStringAsync();
                    var existingOwner = JsonConvert.DeserializeObject<Owner>(ownerData);

                    if (existingOwner != null)
                    {
                        ownerExist = true;
                        model.owner = existingOwner;
                    }
                }
            }

            if (pack.Equals("yearly"))
            {
                price = "300000";
                model.owner.SubscriptionStartDate = DateTime.Now;
                if (model.owner.SubscribeEndDate != null)
                {
                    if (model.owner.SubscribeEndDate < DateTime.Now)
                    {
                        model.owner.SubscribeEndDate = DateTime.Now.AddYears(1);
                    }
                    else
                    {
                        model.owner.SubscribeEndDate = model.owner.SubscribeEndDate.AddYears(1);
                    }
                }
                else
                {
                    model.owner.SubscribeEndDate = DateTime.Now.AddYears(1);
                }
            }
            else if (pack.Equals("monthly"))
            {
                price = "30000";
                model.owner.SubscriptionStartDate = DateTime.Now;
                if (model.owner.SubscribeEndDate != null)
                {
                    if (model.owner.SubscribeEndDate < DateTime.Now)
                    {
                        model.owner.SubscribeEndDate = DateTime.Now.AddMonths(1);
                    }
                    else
                    {
                        model.owner.SubscribeEndDate = model.owner.SubscribeEndDate.AddMonths(1);
                    }
                }
                else
                {
                    model.owner.SubscribeEndDate = DateTime.Now.AddMonths(1);
                }
            }

            if (model.owner != null)
            {
                var ownerJson = JsonConvert.SerializeObject(model.owner);
                var ownerParams = new Dictionary<string, string>
                {
                    { "Price", price },
                    { "Item", "Subscribe" + char.ToUpper(pack[0]) + pack.Substring(1) + "Pack" },
                    { "Message", char.ToUpper(pack[0]) + pack.Substring(1) + "Pack" }
                };

                if (!string.IsNullOrEmpty(ownerJson))
                {
                    Response.Cookies.Append("OwnerData", ownerJson, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(1)
                    });
                }

                Response.Cookies.Append("ItemType", "Subscribe" + char.ToUpper(pack[0]) + pack.Substring(1) + "Pack", new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                });

                redirectUrl += string.Join("&", ownerParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
                var uri = new Uri(redirectUrl);
                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model.owner);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri.AbsoluteUri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string payURL = await response.Content.ReadAsStringAsync();
                        return Redirect(payURL);
                    }
                }
            }
            else
            {
                return RedirectToAction("Error");
            }

            return Redirect(redirectUrl);
        }


        [HttpPost]
        private async Task<IActionResult> SubscribeFreeTrialAction(Model model)
        {
            Employee emp = null;
            Owner owner = null;
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
            model = new Model { owner = model.owner, employee = emp };

            var ownerJson = JsonConvert.SerializeObject(model.owner);
            HttpContext.Response.Cookies.Append("TempLoginInfo", ownerJson, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            bool ownerExist = false;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage getResponse = await client.GetAsync(_apiUrl + $"Owner/Phone/{model.owner.Phone}");
                if (getResponse.IsSuccessStatusCode)
                {
                    var ownerData = await getResponse.Content.ReadAsStringAsync();
                    var existingOwner = JsonConvert.DeserializeObject<Owner>(ownerData);

                    if (existingOwner != null)
                    {
                        if (existingOwner.IsFreeTrial == true)
                        {
                            TempData["Message"] = "Already in free trial";
                            return RedirectToAction("Subscribe", "Home");
                        }
                        else if (existingOwner.IsFreeTrial == false)
                        {
                            TempData["Message"] = "You can only subscribe to the free trial once";
                            return RedirectToAction("Subscribe", "Home");
                        }
                    }
                }
            }
            return View("OTPFreeTrial", model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOtpFreeTrial(string otp)
        {
            var sessionOtp = HttpContext.Session.GetString("OTP");

            // Check if the OTP matches
            if (otp == sessionOtp?.ToString())
            {
                TempData.Remove("OTP");
                try
                {
                    HttpContext.Request.Cookies.TryGetValue("TempLoginInfo", out string loginInfoJson);


                    var owner = JsonConvert.DeserializeObject<Owner>(loginInfoJson);
                    var existingOwner = new Owner();
                    bool ownerExist = false;
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage getResponse = await client.GetAsync(_apiUrl + $"Owner/Phone/{owner.Phone}"))
                        {
                            if (getResponse.IsSuccessStatusCode)
                            {
                                ownerExist = true;
                                var ownerData = await getResponse.Content.ReadAsStringAsync();
                                existingOwner = JsonConvert.DeserializeObject<Owner>(ownerData);
                                if (existingOwner.IsFreeTrial == true)
                                {
                                    TempData["Message"] = "Already in free trial";
                                    return RedirectToAction("Subscribe", "Home");
                                }
                            }
                        }
                        using (HttpResponseMessage getResponse = await client.GetAsync(_apiUrl + $"Owner/Gmail/{owner.Gmail}"))
                        {

                            if (getResponse.IsSuccessStatusCode)
                            {
                                ownerExist = true;
                                var ownerData = await getResponse.Content.ReadAsStringAsync();
                                existingOwner = JsonConvert.DeserializeObject<Owner>(ownerData);
                                if (existingOwner.IsFreeTrial == true)
                                {
                                    TempData["Message"] = "Already in free trial";
                                    return RedirectToAction("Subscribe", "Home");
                                }
                            }
                        }
                    }

                    owner.SubscriptionStartDate = DateTime.Now;
                    owner.SubscribeEndDate = DateTime.Now.AddMonths(1);
                    owner.IsFreeTrial = true;
                    //if (existingOwner != null && existingOwner.IsFreeTrial == true)
                    //{
                    //    TempData["Message"] = "You can only subscribe to the free trial once";
                    //    return RedirectToAction("Subscribe", "Home");
                    //}
                    if (ownerExist == false)
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            using (HttpResponseMessage getResponse = await client.PostAsJsonAsync(_apiUrl + $"Owner/Add", owner))
                            {

                                if (getResponse.IsSuccessStatusCode)
                                {
                                    string mes = await getResponse.Content.ReadAsStringAsync();
                                    owner = JsonConvert.DeserializeObject<Owner>(mes);
                                }
                            }
                        }
                        AddFirstManagerAsync(owner);
                    }
                    
                    
                    var b = JsonConvert.SerializeObject(owner);
                    HttpContext.Response.Cookies.Append("LoginInfo", b, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30)
                    });

                    HttpContext.Response.Cookies.Delete("TempLoginInfo");
                    return RedirectToAction("Index", "Home");
                }
                catch
                {

                }
                return View("OTPFreeTrial");
            }
            else
            {
                var model = new AzzanOrder.ManagerOwner.Models.Model();
                ModelState.AddModelError(string.Empty, "Invalid OTP. Please try again.");
                return View("OTPFreeTrial", model);
            }
        }
        private async Task AddFirstManagerAsync(Owner owner)
        {
            var role = new Role();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage getResponse = await client.GetAsync(_apiUrl + $"Owner/Phone/{owner.Phone}"))
                {

                    if (getResponse.IsSuccessStatusCode)
                    {
                        string mes = await getResponse.Content.ReadAsStringAsync();
                        owner = JsonConvert.DeserializeObject<Owner>(mes);
                    }
                }

                using (HttpResponseMessage addResponse = await client.GetAsync(_apiUrl + $"Role/{1}"))
                {
                    if (addResponse.IsSuccessStatusCode)
                    {
                        string mes = await addResponse.Content.ReadAsStringAsync();
                        role = JsonConvert.DeserializeObject<Role>(mes);
                    }
                }
                
                var emp = new Employee() { EmployeeName = owner.OwnerName, Phone = owner.Phone, Gender = owner.Gender, Gmail = owner.Gmail, BirthDate = owner.BirthDate, RoleId = 1, Image = owner.Image, IsDelete = false, OwnerId = owner.OwnerId, Role = role };
                Console.WriteLine(JsonConvert.SerializeObject(emp));
                using (HttpResponseMessage addResponse = await client.PostAsJsonAsync(_apiUrl + "Employee/Add", emp))
                {
                    if (addResponse.IsSuccessStatusCode)
                    {
                        string addMessage = await addResponse.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Employee>(addMessage);
                    }
                }
                var a = new ItemCategory() { ItemCategoryName = "LỚP PHỦ", Description = "TOPPING", Discount = 0, EmployeeId = emp.EmployeeId, IsCombo = false, IsDelete = false };

                using (HttpResponseMessage addResponse = await client.PostAsJsonAsync(_apiUrl + "ItemCategory/Add", a))
                {
                    if (addResponse.IsSuccessStatusCode)
                    {
                        string addMessage = await addResponse.Content.ReadAsStringAsync();
                    }
                }
                var c = new Table() { EmployeeId = emp.EmployeeId, Qr = "QR_000", Status = true };
                using (HttpResponseMessage addResponse = await client.PostAsJsonAsync(_apiUrl + "Table/Add", c))
                {
                    if (addResponse.IsSuccessStatusCode)
                    {
                        string addMessage = await addResponse.Content.ReadAsStringAsync();
                    }
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> OwnerRegister([FromBody] Owner owner)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage res = await client.GetAsync(_apiUrl + "/Employee/FirstEmployee/" + owner.OwnerId);
                        if (!res.IsSuccessStatusCode)
                        {
                            AddFirstManagerAsync(owner);
                        }
                    }
                    catch (HttpRequestException e) { }
                }            
                var ownerJson = JsonConvert.SerializeObject(owner);
                HttpContext.Response.Cookies.Append("LoginInfo", ownerJson, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(30)
                });
                return Ok();
            }
            catch
            {
            }
            return Conflict();
        }

        public async Task<IActionResult> SubscribeExtension()
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string loginInfoJson);
                var owner = JsonConvert.DeserializeObject<Owner>(loginInfoJson);
                TempData["EndDate"] = "Your subscription is until " + owner.SubscribeEndDate.ToString("MMMM dd, yyyy") + ". " + (owner.SubscribeEndDate - DateTime.Now).Days + " days remaining.";

                if ((owner.SubscribeEndDate - DateTime.Now).Days <= 7)
                {
                    TempData["EndDateWarn"] = "Your subscription is about to expire in " + (owner.SubscribeEndDate - DateTime.Now).Days + " days.";
                }

                return View();
            }
            else if (loginStatus.Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
            }
            else if (loginStatus.Equals("owner expired"))
            {
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubscribeExtension(string pack)
        {

            Model model = new Model();
            model.owner = new Owner();
            HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string loginInfoJson);
            if (!string.IsNullOrEmpty(loginInfoJson))
            {
                model.owner = JsonConvert.DeserializeObject<Owner>(loginInfoJson);
            }
            else
            {
                return View();
            }

            if (string.IsNullOrEmpty(model.owner.Bank.PAYOS_CLIENT_ID) || string.IsNullOrEmpty(model.owner.Bank.PAYOS_API_KEY) || string.IsNullOrEmpty(model.owner.Bank.PAYOS_CHECKSUM_KEY) || string.IsNullOrEmpty(model.owner.Phone) || string.IsNullOrEmpty(model.owner.Gmail))
            {
                TempData["Message"] = "Owner PAYOS information, phone number, or Gmail is missing.";
                return RedirectToAction("SubscribeExtension", "Home");
            }



            string redirectUrl = new Config()._payOS + "Subscribe/?";
            string price = "0";

            bool ownerExist = false;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage getResponse = await client.GetAsync(_apiUrl + $"Owner/Phone/{model.owner.Phone}");
                if (getResponse.IsSuccessStatusCode)
                {
                    var ownerData = await getResponse.Content.ReadAsStringAsync();
                    var existingOwner = JsonConvert.DeserializeObject<Owner>(ownerData);
                    if (existingOwner != null)
                    {
                        ownerExist = true;
                        model.owner = existingOwner;
                    }
                }
            }

            if (string.IsNullOrEmpty(model.owner.Bank.PAYOS_CLIENT_ID) || string.IsNullOrEmpty(model.owner.Bank.PAYOS_API_KEY) || string.IsNullOrEmpty(model.owner.Bank.PAYOS_CHECKSUM_KEY) || string.IsNullOrEmpty(model.owner.Phone))
            {
                TempData["Message"] = "Owner PAYOS information or phone number is missing.";
                return RedirectToAction("SubscribeExtension", "Home");
            }


            if (pack.Equals("yearly"))
            {
                price = "300000";
                model.owner.SubscriptionStartDate = DateTime.Now;
                //model.owner.SubscribeEndDate = DateTime.Now.AddYears(1);
                if (model.owner.SubscribeEndDate != null)
                {
                    if (model.owner.SubscribeEndDate < DateTime.Now)
                    {
                        model.owner.SubscribeEndDate = DateTime.Now.AddYears(1);
                    }
                    else
                    {
                        model.owner.SubscribeEndDate = model.owner.SubscribeEndDate.AddYears(1);
                    }
                }
                else
                {
                    model.owner.SubscribeEndDate = DateTime.Now.AddYears(1);
                }
            }
            else if (pack.Equals("forever"))
            {
                price = "3000000";
                model.owner.SubscriptionStartDate = DateTime.Now;
                model.owner.SubscribeEndDate = DateTime.Now.AddYears(100);
            }

            if (model.owner != null)
            {
                var ownerJson = JsonConvert.SerializeObject(model.owner);
                var ownerParams = new Dictionary<string, string>
                {
                    { "Price", price },
                    { "Item", "Subscribe" + char.ToUpper(pack[0]) + pack.Substring(1) + "Pack" },
                    { "Message", char.ToUpper(pack[0]) + pack.Substring(1) + "Pack" }
                };

                if (!string.IsNullOrEmpty(ownerJson))
                {
                    Response.Cookies.Append("OwnerData", ownerJson, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(1)
                    });
                }

                Response.Cookies.Append("ItemType", "Subscribe" + char.ToUpper(pack[0]) + pack.Substring(1) + "Pack", new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                });

                redirectUrl += string.Join("&", ownerParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
                var uri = new Uri(redirectUrl);
                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model.owner);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri.AbsoluteUri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string payURL = await response.Content.ReadAsStringAsync();
                        return Redirect(payURL);
                    }
                }
            }
            else
            {
                return RedirectToAction("Error");
            }

            return Redirect(redirectUrl);
        }

        public async Task<IActionResult> NotificationRead(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Get the notification by id
                var response = await client.GetAsync(_apiUrl + $"Notification/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var notificationJson = await response.Content.ReadAsStringAsync();
                    var notification = JsonConvert.DeserializeObject<Notification>(notificationJson);

                    if (notification != null)
                    {
                        // Change the IsRead property to true
                        notification.IsRead = true;

                        // Update the notification
                        var content = new StringContent(JsonConvert.SerializeObject(notification), Encoding.UTF8, "application/json");
                        await client.PutAsync(_apiUrl + $"Notification/Update", content);
                    }
                }
            }

            // Redirect to the previous page
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Profile(Model model)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {

            }
            else if (loginStatus.Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
            }
            else if (loginStatus.Equals("owner expired"))
            {
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                return RedirectToAction("Login", "Home");
            }
            Owner emp = new Owner();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Owner>(empJson);
            }
            using (HttpClient client = new HttpClient())
            {
                // Get the notification by id
                var response = await client.GetAsync(_apiUrl + $"Owner/{emp.OwnerId}");
                if (response.IsSuccessStatusCode)
                {
                    var notificationJson = await response.Content.ReadAsStringAsync();
                    emp = JsonConvert.DeserializeObject<Owner>(notificationJson);
                }
            }
            if (model.owner == null)
            {
                model.owner = emp;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(Model model, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    model.owner.Image = "data:image/png;base64," + Convert.ToBase64String(fileBytes);
                }
            }

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Owner/Phone/{model.owner.Phone}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        string mess = await res.Content.ReadAsStringAsync();
                        var a = JsonConvert.DeserializeObject<Owner>(mess);
                        if (a.OwnerId != model.owner.OwnerId)
                        {
                            TempData["ErrorPhone"] = "Phone is already in use.";
                        }
                    }
                }
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Owner/Gmail/{model.owner.Gmail}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        string mess = await res.Content.ReadAsStringAsync();
                        var a = JsonConvert.DeserializeObject<Owner>(mess);
                        if (a.OwnerId != model.owner.OwnerId)
                        {
                            TempData["ErrorGmail"] = "Email is already in use.";
                        }
                    }
                }
                if (TempData["ErrorPhone"] != null || TempData["ErrorGmail"] != null)
                {
                    return View(new Model() { owner = model.owner });
                }
                var json = JsonConvert.SerializeObject(model.owner);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(_apiUrl + "Owner/Update", content);

                if (response.IsSuccessStatusCode)
                {
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


