using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class ManagerController : Controller
    {
        private readonly string _apiUrl = new Config()._apiUrl;
        public async Task<IActionResult> List(int? page)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {

            }
            else if (authorizeLogin.Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
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
            List<Employee> employees = new List<Employee>();
            int totalEmployees = 0;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee");
                    string data = await res.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<List<Employee>>(data);
                    totalEmployees = employees.Count(e => e.IsDelete == false);
                }
                catch (HttpRequestException e)
                {
                    // Handle the exception here
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }
            }
            employees = employees.Where(e => e.RoleId == 1).ToList();

            int pageSize = 10;
            int pageNumber = page ?? 1;
            int maxPageNav = 10;
            totalEmployees = employees.Count();
            int totalPages = (int)Math.Ceiling((double)totalEmployees / pageSize);

            // Paginate the employees
            employees = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new Model
            {
                anIntegerUsedForCountingNumberOfPageQueuedForTheList = totalPages,
                anIntegerUsedForKnowingWhatTheCurrentPageOfTheList = pageNumber,
                thisIntegerIsUsedForKnowingTheMaxNumberOfPageNavButtonShouldBeDisplayed = maxPageNav,
                employees = employees,
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {

            }
            else if (authorizeLogin.Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
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
        public async Task<IActionResult> AddPost(Employee employee, IFormFile Image)
        {
            Owner emp = new Owner();
            var e = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Owner>(empJson);
            }
            //if (Image != null && Image.Length > 0)
            //{
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        await Image.CopyToAsync(memoryStream);
            //        var fileBytes = memoryStream.ToArray();
            //        employee.Image = "data:image/png;base64," + Convert.ToBase64String(fileBytes);
            //    }
            //}
            employee.RoleId = 1;
            employee.OwnerId = emp.OwnerId;
            employee.IsDelete = false;
            using (HttpClient client = new HttpClient())
            {
             

                HttpResponseMessage response = await client.PostAsJsonAsync(_apiUrl + "Employee/Add/", employee);

                if (response.IsSuccessStatusCode)
                {
                    HttpResponseMessage res = await client.GetAsync(_apiUrl + "/Employee/FirstEmployee/" + emp.OwnerId);
                    if (res.IsSuccessStatusCode)
                    {
                        string a = await res.Content.ReadAsStringAsync();
                        e = JsonConvert.DeserializeObject<Employee>(a);
                        HttpResponseMessage res1 = await client.GetAsync(_apiUrl + $"/MenuItem/GetAllMenuItem?employeeId={e.EmployeeId}");
                        if (res1.IsSuccessStatusCode)
                        {
                            string b = await res.Content.ReadAsStringAsync();
                            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(b);
                            foreach (var i in menuItems)
                            {
                                i.EmployeeId = e.EmployeeId;
                                HttpResponseMessage res2 = await client.PostAsJsonAsync(_apiUrl + "/MenuItem/Add", i);
                                if (res2.IsSuccessStatusCode)
                                {
                                    string c = await res2.Content.ReadAsStringAsync();
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
               
                
            }
            
            return View(employee);
        }

        public async Task<IActionResult> Update(int id)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {

            }
            else if (authorizeLogin.Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
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
            Employee employee = new Employee();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/" + id);
                    if (res.IsSuccessStatusCode)
                    {
                        string data = await res.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(data);
                    }
                    else
                    {
                        if (employee.RoleId != 1)
                        {
                            return RedirectToAction("List");
                        }
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
            return View(employee);
        }

        // POST: Employee/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Employee employee, IFormFile Image)
        {
            Owner emp = new Owner();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Owner>(empJson);
            }
            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    employee.Image = "data:image/png;base64," + Convert.ToBase64String(fileBytes);
                }
            }
            employee.RoleId = 1;
            employee.OwnerId = emp.OwnerId;
            employee.IsDelete = false;
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(employee);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(_apiUrl + "Employee/Update", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employee);
        }

        public async Task<IActionResult> MenuList(int id, int? page)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            if (authorizeLogin.Equals("owner"))
            {

            }
            else if (authorizeLogin.Equals("manager"))
            {
                return RedirectToAction("List", "Employee");
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
            List<ItemCategory> itemCategories = new List<ItemCategory>();
            var menuItemCounts = 0;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url1 = _apiUrl + $"ItemCategory?id={id}";
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
            int pageSize = 10;
            int pageNumber = page ?? 1;
            int maxPageNav = 10;
            int totalTables = itemCategories.SelectMany(x => x.MenuCategories).Count();
            int totalPages = (int)Math.Ceiling((double)totalTables / pageSize);

            var paginatedMenuCategories = itemCategories
                .SelectMany(x => x.MenuCategories)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var viewModel = new Model
            {
                anIntegerUsedForCountingNumberOfPageQueuedForTheList = totalPages,
                anIntegerUsedForKnowingWhatTheCurrentPageOfTheList = pageNumber,
                thisIntegerIsUsedForKnowingTheMaxNumberOfPageNavButtonShouldBeDisplayed = maxPageNav,
                itemCategories = itemCategories,
                menuCategories = paginatedMenuCategories,
                employee = new Employee
                {
                    EmployeeId = id
                }
            };
            return View(viewModel);
        }
    }
}
