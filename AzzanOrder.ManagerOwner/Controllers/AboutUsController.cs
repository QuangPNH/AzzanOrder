using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly HttpClient _httpClient;
        public AboutUsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private readonly string _apiUrl = "https://localhost:7183/api/";
        public async Task<IActionResult> ListAsync(int? page)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {

            }
            else if (authorizeLogin.Equals("manager"))
            {
                return RedirectToAction("Index", "Home");
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
            Owner emp = new Owner();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Owner>(empJson);
            }
            List<About> abouts = new List<About>();
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    HttpResponseMessage res = await client.GetAsync(_apiUrl + "Abouts?ownerId=" + emp.OwnerId);
                    if (res.IsSuccessStatusCode)
                    {
                        string data = await res.Content.ReadAsStringAsync();
                        abouts = JsonConvert.DeserializeObject<List<About>>(data);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                catch (HttpRequestException)
                {
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }
            }
            int pageSize = 10;
            int pageNumber = page ?? 1;
            int maxPageNav = 10;
            int totalTables = abouts.Count;
            int totalPages = (int)Math.Ceiling((double)totalTables / pageSize);
            abouts = abouts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var viewModel = new Model
            {
                anIntegerUsedForCountingNumberOfPageQueuedForTheList = totalPages,
                anIntegerUsedForKnowingWhatTheCurrentPageOfTheList = pageNumber,
                thisIntegerIsUsedForKnowingTheMaxNumberOfPageNavButtonShouldBeDisplayed = maxPageNav,
                abouts = abouts,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> UpdateAsync(int id)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {

            }
            else if (authorizeLogin.Equals("manager"))
            {
                return RedirectToAction("Index", "Home");
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
            Owner emp = new Owner();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Owner>(empJson);
            }
            About a = new About();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(_apiUrl + "Abouts/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        a = JsonConvert.DeserializeObject<About>(data);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                catch (HttpRequestException)
                {
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }
            }
            Model model = new Model()
            {
                about = a
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult AddAsync()
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {

            }
            else if (authorizeLogin.Equals("manager"))
            {
                return RedirectToAction("Index", "Home");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(About about)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {

            }
            else if (authorizeLogin.Equals("manager"))
            {
                return RedirectToAction("Index", "Home");
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
            about.OwnerId = emp.OwnerId;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(_apiUrl + "Abouts/Add/", about))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string message = await res.Content.ReadAsStringAsync();
                            about = JsonConvert.DeserializeObject<About>(message);

                        }
                    }
                }
                catch (HttpRequestException)
                {
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }

            }
            return RedirectToAction("List", "AboutUs");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(About about)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {

            }
            else if (authorizeLogin.Equals("manager"))
            {
                return RedirectToAction("Index", "Home");
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
            Owner emp = new Owner();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Owner>(empJson);
            }
            about.OwnerId = emp.OwnerId;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    using (HttpResponseMessage res = await client.PutAsJsonAsync(_apiUrl + "Abouts/Update/", about))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string message = await res.Content.ReadAsStringAsync();

                        }
                    }
                }
                catch (HttpRequestException)
                {
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }

            }
            // If model validation fails, redisplay the form with validation messages
            return RedirectToAction("List", "AboutUs");
        }
        public async Task<IActionResult> DeleteAsync(int id)
        {
    

            Owner emp = new Owner();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Owner>(empJson);
            }
            using (HttpClient client = new HttpClient())
            {

                using (HttpResponseMessage res = await client.DeleteAsync(_apiUrl + "Abouts/Delete/" + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                    }
                }

            }
            return RedirectToAction("List", "AboutUs");
        }
    }
}