using AzzanOrder.ManagerOwner.Models; // Update this line
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Dynamic;
using System.Reflection;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class PromotionController : Controller
    {
        private string urlPromotion = "https://localhost:7183/api/Promotions/GetByDescription/";

        [HttpGet("ListAll")]
        public async Task<IActionResult> ListAll(int manaId = 1)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (authorizeLogin.Equals("manager"))
            {
            }
            else if (authorizeLogin.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (authorizeLogin.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            dynamic promotions = new ExpandoObject();
            promotions.Logo = new Promotion();
            promotions.BackgroundColor = new Promotion();
            promotions.Carousel = new List<Promotion>();
            promotions.Banner = new List<Promotion>();
            promotions.Hotline = new Promotion();
            promotions.Mail = new Promotion();
            promotions.Contact = new Promotion();
            promotions.Cart = new Promotion();
            promotions.Promotions = new List<Promotion>();

            manaId = emp.EmployeeId;
            var endpoints = new Dictionary<string, Action<string>>
            {
                { $"logo?manaId={manaId}", response => promotions.Logo = JsonConvert.DeserializeObject<Promotion>(response) },
                { $"color?manaId={manaId}", response => promotions.BackgroundColor = JsonConvert.DeserializeObject<Promotion>(response) },
                { $"carousel?manaId={manaId}", response => promotions.Carousel = JsonConvert.DeserializeObject<List<Promotion>>(response) },
                { $"banner?manaId={manaId}", response => promotions.Banner = JsonConvert.DeserializeObject<List<Promotion>>(response) },
                { $"hotline?manaId={manaId}", response => promotions.Hotline = JsonConvert.DeserializeObject < Promotion >(response) },
                { $"mail?manaId={manaId}", response => promotions.Mail = JsonConvert.DeserializeObject < Promotion >(response) },
                { $"contact?manaId={manaId}", response => promotions.Contact = JsonConvert.DeserializeObject<Promotion>(response) },
                { $"cart?manaId={manaId}", response => promotions.Cart = JsonConvert.DeserializeObject<Promotion>(response) }
            };

            using (var httpClient = new HttpClient())
            {
                foreach (var endpoint in endpoints)
                {
                    try
                    {
                        var response = await httpClient.GetStringAsync(urlPromotion + endpoint.Key);
                        endpoint.Value(response);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception if necessary
                    }
                }
                var response1 = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetPromotionsByEmpId/" + emp.EmployeeId);
                promotions.Promotions = JsonConvert.DeserializeObject<List<Promotion>>(response1);
            }

            return View(promotions);
        }

        public IActionResult Add()
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (authorizeLogin.Equals("manager"))
            {
            }
            else if (authorizeLogin.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (authorizeLogin.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        // POST: Employee/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Promotion promotion, IFormFile? Image)
        {
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    promotion.Image = "data:image/png;base64," + Convert.ToBase64String(fileBytes);
                }
            }

            // Combine Destination and Description
            var destination = Request.Form["Destination"];
            promotion.Description = $"{destination}/{promotion.Description}";
            promotion.EmployeeId = emp.EmployeeId;

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(promotion);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://localhost:7183/api/Promotions/Add", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListAll");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(promotion);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, string destination)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (authorizeLogin.Equals("manager"))
            {
            }
            else if (authorizeLogin.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (authorizeLogin.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }
            Promotion table = new Promotion();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:7183/api/Promotions/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        table = JsonConvert.DeserializeObject<Promotion>(data);

                        if (table != null && !string.IsNullOrEmpty(table.Description))
                        {
                            var parts = table.Description.Split('/');
                            ViewBag.Destination = parts[0].IsNullOrEmpty() ? destination : parts[0];
                            table.Description = parts[1] ?? string.Empty;
                        }
                    }
                    else
                    {
                        ViewBag.Destination = destination;
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                catch (HttpRequestException)
                {
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }
            }
            return View(table);
        }

        // POST: Employee/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Promotion promotion, IFormFile? Image)
        {
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Image.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        promotion.Image = "data:image/png;base64," + Convert.ToBase64String(fileBytes);
                    }
                }

                // Combine Destination and Description
                var destination = Request.Form["Destination"];
                promotion.Description = $"{destination}/{promotion.Description}";
                promotion.EmployeeId = emp.EmployeeId;

                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(promotion);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response;
                    if (promotion.PromotionId == 0)
                    {
                        response = await client.PostAsync("https://localhost:7183/api/Promotions/Add", content);
                    }
                    else
                    {
                        response = await client.PutAsync("https://localhost:7183/api/Promotions/Update", content);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListAll");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }
            return View(promotion);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7183/api/Promotions/Delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListAll");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return RedirectToAction("ListAll");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveStatus([FromBody] object data)
        {
            // Handle the save status logic here
            // For example, update the database with the new status

            return Ok();
        }
    }
}
