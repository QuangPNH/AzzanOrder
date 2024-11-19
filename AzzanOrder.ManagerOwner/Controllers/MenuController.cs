using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
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

        private readonly string _apiUrl = "https://localhost:7183/api/";

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
            List<MenuItem> menuItems = new List<MenuItem>();
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
                    var url = emp != null ? _apiUrl + $"MenuItem/GetAllMenuItem?employeeId={emp.EmployeeId}" : _apiUrl + "MenuItem/GetAllMenuItem";
                    HttpResponseMessage menuItemRes = await client.GetAsync(url);
                    if (menuItemRes.IsSuccessStatusCode)
                    {
                        string menuItemData = await menuItemRes.Content.ReadAsStringAsync();
                        menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(menuItemData);
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

            //int pageSize = 10;
            //int pageNumber = page ?? 1;
            //int maxPageNav = 10;
            //menuItemCounts = menuItems.Count();
            //int totalPages = (int)Math.Ceiling((double)menuItemCounts / pageSize);

            //// Paginate the employees
            //menuItems = menuItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            Model model = new Model
            {
                //anIntegerUsedForCountingNumberOfPageQueuedForTheList = totalPages,
                //anIntegerUsedForKnowingWhatTheCurrentPageOfTheList = pageNumber,
                //thisIntegerIsUsedForKnowingTheMaxNumberOfPageNavButtonShouldBeDisplayed = maxPageNav,
                itemCategories = itemCategories,
                menuItems = menuItems
            };
            //foreach(var i in menuItems)
            //{
                
            //   foreach(var j in i.MenuCategories.Where(mc => mc.MenuItemId == i.MenuItemId).ToList())
            //    {
            //        Console.WriteLine(j.ItemCategory.ItemCategoryName);
            //    }
            //}
            return View(model);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("List");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }

        public IActionResult ListItem()
        {
            return View();
        }
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItemPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ListItem");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }
        public IActionResult EditItem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItemPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ListItem");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }
    }
}
