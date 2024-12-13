using AzzanOrder.ManagerOwner.DTOs;
using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Twilio.TwiML.Voice;
using static System.Net.Mime.MediaTypeNames;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class ManagerController : Controller
    {
        private readonly string _apiUrl = new Models.Config()._apiUrl;
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
            employees = employees.Where(e => e.RoleId == 1 && e.IsDelete == false && e.OwnerId == emp.OwnerId).ToList();

            var viewModel = new Model
            {

                employees = employees
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
            var viewModel = new Model
            {
                employee = new Employee()
            };
            return View(viewModel);
        }

        // POST: Employee/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Employee employee)
        {
            Owner emp = new Owner();
            var e = new Employee();
            var role = new Role();
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
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage addResponse = await client.GetAsync(_apiUrl + $"Role/{1}"))
                {
                    if (addResponse.IsSuccessStatusCode)
                    {
                        string mes = await addResponse.Content.ReadAsStringAsync();
                        role = JsonConvert.DeserializeObject<Role>(mes);
                    }
                }
            }
            employee.OwnerId = emp.OwnerId;
            employee.IsDelete = false;
            employee.Role = role;

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Employee/Phone/{employee.Phone}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["ErrorPhone"] = "Phone is already in use.";
                    }
                }
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Employee/Gmail/{employee.Gmail}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["ErrorGmail"] = "Email is already in use.";

                    }
                }



                if (TempData["ErrorPhone"] != null || TempData["ErrorGmail"] != null)
                {
                    return View(new Model() { employee = employee });
                }


                using (HttpResponseMessage response = await client.PostAsJsonAsync(_apiUrl + "Employee/Add/", employee))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string g = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(g);
                        HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Employee/FirstEmployee/{emp.OwnerId}");
                        if (res.IsSuccessStatusCode)
                        {
                            string a = await res.Content.ReadAsStringAsync();
                            e = JsonConvert.DeserializeObject<Employee>(a);
                            HttpResponseMessage res1 = await client.GetAsync(_apiUrl + $"ItemCategory/GetAllBaseItemCategories?id={e.EmployeeId}");
                            if (res1.IsSuccessStatusCode)
                            {
                                string b = await res1.Content.ReadAsStringAsync();
                                var itemCategories = JsonConvert.DeserializeObject<List<ItemCategory>>(b);
                                AddBaseMenu(itemCategories, employee);
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
                        return RedirectToAction("List", "Manager");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }
            return View(new Model() { employee = employee });
        }
        private async void AddBaseMenu(List<ItemCategory> itemCategories, Employee employee)
        {
            using (HttpClient client = new HttpClient())
            {
                var t = new Table() { EmployeeId = employee.EmployeeId, Qr = "QR_000", Status = true };
                using (HttpResponseMessage addResponse = await client.PostAsJsonAsync(_apiUrl + "Table/Add", t))
                {
                    if (addResponse.IsSuccessStatusCode)
                    {
                        string addMessage = await addResponse.Content.ReadAsStringAsync();
                    }
                }
                foreach (var i in itemCategories)
                {
                    ItemCategory category = new ItemCategory()
                    {
                        ItemCategoryName = i.ItemCategoryName,
                        Description = i.Description,
                        Discount = i.Discount,
                        Image = i.Image,
                        EmployeeId = employee.EmployeeId,
                        IsDelete = i.IsDelete,
                        StartDate = i.StartDate,
                        EndDate = i.EndDate,
                        IsCombo = i.IsCombo
                    };
                    HttpResponseMessage res2 = await client.PostAsJsonAsync(_apiUrl + "ItemCategory/Add/", category);
                    if (res2.IsSuccessStatusCode)
                    {
                        string c = await res2.Content.ReadAsStringAsync();
                        var itemCategory = JsonConvert.DeserializeObject<ItemCategory>(c);
                        foreach (var j in i.MenuCategories.Where(mc => mc.ItemCategoryId == i.ItemCategoryId).Select(mc => mc.MenuItem).ToList())
                        {
                            MenuItem item = new MenuItem()
                            {
                                ItemName = j.ItemName,
                                Price = j.Price,
                                Description = j.Description,
                                Discount = j.Discount,
                                Image = j.Image,
                                IsAvailable = j.IsAvailable,
                                EmployeeId = employee.EmployeeId
                            };
                            HttpResponseMessage res3 = await client.PostAsJsonAsync(_apiUrl + "MenuItem/Add/", item);
                            if (res3.IsSuccessStatusCode)
                            {
                                string d = await res3.Content.ReadAsStringAsync();
                                var menuItem = JsonConvert.DeserializeObject<MenuItem>(d);
                                HttpResponseMessage res4 = await client.PostAsJsonAsync(_apiUrl + "MenuCategory/Add/", new MenuCategoryDTO() { MenuItemId = menuItem.MenuItemId, ItemCategoryId = itemCategory.ItemCategoryId });
                                if (res4.IsSuccessStatusCode)
                                {
                                    string f = await res4.Content.ReadAsStringAsync();
                                    var menuCategory = JsonConvert.DeserializeObject<MenuCategory>(f);
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
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }
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
            Role role = new Role();
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
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage addResponse = await client.GetAsync(_apiUrl + $"Role/{1}"))
                {
                    if (addResponse.IsSuccessStatusCode)
                    {
                        string mes = await addResponse.Content.ReadAsStringAsync();
                        role = JsonConvert.DeserializeObject<Role>(mes);
                    }
                }
            }
            employee.OwnerId = emp.OwnerId;
            employee.IsDelete = false;
            employee.Role = role;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Employee/Phone/{employee.Phone}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        string mess = await res.Content.ReadAsStringAsync();
                        var a = JsonConvert.DeserializeObject<Employee>(mess);
                        if (a.EmployeeId != employee.EmployeeId)
                        {
                            TempData["ErrorPhone"] = "Phone is already in use.";
                        }
                    }
                }
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Employee/Gmail/{employee.Gmail}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        string mess = await res.Content.ReadAsStringAsync();
                        var a = JsonConvert.DeserializeObject<Employee>(mess);
                        if (a.EmployeeId != employee.EmployeeId)
                        {
                            TempData["ErrorGmail"] = "Email is already in use.";
                        }
                    }
                }
                
                if (TempData["ErrorPhone"] != null || TempData["ErrorGmail"] != null)
                {
                    return View(new Model() { employee = employee });
                }
                
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.DeleteAsync(_apiUrl + "Employee/Delete/" + id);
                string mess = await res.Content.ReadAsStringAsync();
                ViewBag.error = mess;

                return RedirectToAction("List", "Manager");
            }
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
