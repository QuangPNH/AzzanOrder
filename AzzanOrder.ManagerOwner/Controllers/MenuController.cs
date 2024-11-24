using AzzanOrder.ManagerOwner.DTOs;
using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class MenuController : Controller
    {

        private readonly HttpClient _httpClient;

        public MenuController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiUrl = new Config()._apiUrl;

        public async Task<IActionResult> ListAsync(int? page)
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
            List<ItemCategory> itemCategories = new List<ItemCategory>();
            var menuItemCounts = 0;
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url1 = emp != null ? _apiUrl + $"ItemCategory?id={emp.EmployeeId}" : _apiUrl + "ItemCategory";
                    HttpResponseMessage itemCategoryRes = await client.GetAsync(url1);
                    if (itemCategoryRes.IsSuccessStatusCode)
                    {
                        string itemCategoryData = await itemCategoryRes.Content.ReadAsStringAsync();
                        itemCategories = JsonConvert.DeserializeObject<List<ItemCategory>>(itemCategoryData);
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
            Model model = new Model
            {
                itemCategories = itemCategories
            };
            return View(model);
        }



       
        public async Task<IActionResult> AddAsync()
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
            List<ItemCategory> itemCategories = new List<ItemCategory>();
            var menuItemCounts = 0;
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url1 = emp != null ? _apiUrl + $"ItemCategory?id={emp.EmployeeId}" : _apiUrl + "ItemCategory";
                    HttpResponseMessage itemCategoryRes = await client.GetAsync(url1);
                    if (itemCategoryRes.IsSuccessStatusCode)
                    {
                        string itemCategoryData = await itemCategoryRes.Content.ReadAsStringAsync();
                        itemCategories = JsonConvert.DeserializeObject<List<ItemCategory>>(itemCategoryData);
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
            Model model = new Model
            {
                itemCategories = itemCategories
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(MenuItem menuItem)
        {
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            menuItem.EmployeeId = emp.EmployeeId;
            //List<Voucher> voucherlist = new List<Voucher>();
            var itemCategories = new List<ItemCategory>();
            var selectedCategories = Request.Form["SelectedCategories"].Select(int.Parse).ToList();
            //var isAvailable = !Request.Form["IsAvailable"].IsNullOrEmpty();
            menuItem.IsAvailable = true;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(_apiUrl + "MenuItem/Add/", menuItem))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                        menuItem = JsonConvert.DeserializeObject<MenuItem>(message);

                    }
                }

                foreach (var i in selectedCategories)
                {
                    using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"MenuCategory/itemCategoryId/menuItemId?itemCategoryId={i}&menuItemId={menuItem.MenuItemId}"))
                    {
                        if (!res.IsSuccessStatusCode)
                        {
                            using (HttpResponseMessage res1 = await client.PostAsJsonAsync(_apiUrl + "MenuCategory/Add/", new MenuCategoryDTO() { ItemCategoryId = i, MenuItemId = menuItem.MenuItemId }))
                            {
                                using (HttpContent content = res1.Content)
                                {
                                    string message = await res.Content.ReadAsStringAsync();

                                }
                            }
                        }
                    }
                }
            }
            // If model validation fails, redisplay the form with validation messages
            return RedirectToAction("List", "Menu");
        }
    
        public async Task<IActionResult> UpdateAsync(int id)
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
            List<ItemCategory> itemCategories = new List<ItemCategory>();
            MenuItem mi = new MenuItem();
            var menuItemCounts = 0;
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url1 = emp != null ? _apiUrl + $"ItemCategory?id={emp.EmployeeId}" : _apiUrl + "ItemCategory";
                    HttpResponseMessage itemCategoryRes = await client.GetAsync(url1);
                    if (itemCategoryRes.IsSuccessStatusCode)
                    {
                        string itemCategoryData = await itemCategoryRes.Content.ReadAsStringAsync();
                        itemCategories = JsonConvert.DeserializeObject<List<ItemCategory>>(itemCategoryData);
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
                try
                {
                    var url = _apiUrl + $"MenuItem/{id}";
                    HttpResponseMessage menuItem = await client.GetAsync(url);
                    if (menuItem.IsSuccessStatusCode)
                    {
                        string menuItemStr = await menuItem.Content.ReadAsStringAsync();
                        mi = JsonConvert.DeserializeObject<MenuItem>(menuItemStr);
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
            Model model = new Model
            {
                itemCategories = itemCategories,
                menuItem = mi
            };
            foreach (var i in itemCategories)
            {
                var a = mi.MenuCategories.Any(v => v.ItemCategoryId == i.ItemCategoryId && v.MenuItemId == mi.MenuItemId);
            }
            foreach (var i in mi.MenuCategories)
            {

                    Console.WriteLine(i.ItemCategory.ItemCategoryName);
                
            }
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(MenuItem menuItem)
        {
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            menuItem.EmployeeId = emp.EmployeeId;
            //List<Voucher> voucherlist = new List<Voucher>();
            var itemCategories = new List<ItemCategory>();
            var selectedCategories = Request.Form["SelectedCategories"].Select(int.Parse).ToList();
            //var isAvailable = !Request.Form["IsAvailable"].IsNullOrEmpty();
            menuItem.IsAvailable = true;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PutAsJsonAsync(_apiUrl + "MenuItem/Update/", menuItem))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                        menuItem = JsonConvert.DeserializeObject<MenuItem>(message);

                    }
                }

                foreach (var i in selectedCategories)
                {
                    using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"MenuCategory/itemCategoryId/menuItemId?itemCategoryId={i}&menuItemId={menuItem.MenuItemId}"))
                    {
                        if (!res.IsSuccessStatusCode)
                        {
                            using (HttpResponseMessage res1 = await client.PostAsJsonAsync(_apiUrl + "MenuCategory/Add/", new MenuCategoryDTO() { ItemCategoryId = i, MenuItemId = menuItem.MenuItemId}))
                            {
                                using (HttpContent content = res1.Content)
                                {
                                    string message = await res.Content.ReadAsStringAsync();
                                  
                                }
                            }
                        }
                    }        
                }
            }
                // If model validation fails, redisplay the form with validation messages
                return RedirectToAction("List", "Menu");
        }
    }
}
