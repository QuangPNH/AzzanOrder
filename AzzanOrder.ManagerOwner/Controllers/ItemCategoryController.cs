﻿using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace AzzanOrder.ManagerOwner.Controllers
{
	public class ItemCategoryController : Controller
	{
        private readonly HttpClient _httpClient;

        public ItemCategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private readonly string _apiUrl = new Config()._apiUrl;
        public async Task<IActionResult> ListAsync(int? page)
		{
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
            }
            else if (loginStatus.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            List<ItemCategory> tables = new List<ItemCategory>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage tableRes = await client.GetAsync(_apiUrl + $"ItemCategory?id={emp.EmployeeId}");
                    if (tableRes.IsSuccessStatusCode)
                    {
                        string tableData = await tableRes.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<List<ItemCategory>>(tableData) ?? new List<ItemCategory>();
                        tables = data.Where(x => x.IsDelete != true).ToList(); 
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

            var viewModel = new Model
            {

                itemCategories = tables
            };
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
		{
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
            }
            else if (loginStatus.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }
            return View();
		}

	
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(ItemCategory itemCategory)
		{
            if (itemCategory.Description.ToLower().Contains("topping"))
            {
                TempData["ErrorMes"] = "Topping is exist and only have one";
                return View(itemCategory);
            }
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            var isCombo = !Request.Form["IsCombo"].IsNullOrEmpty();
            if (ModelState.IsValid)
			{
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        itemCategory.IsCombo = isCombo;
                        itemCategory.EmployeeId = emp.EmployeeId;
                        itemCategory.IsDelete = false;
                        HttpResponseMessage response = await client.PostAsJsonAsync(_apiUrl + "ItemCategory/Add", itemCategory);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("List");
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
			}
			return View();
		}

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
            }
            else if (loginStatus.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }
            ItemCategory table = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(_apiUrl + "ItemCategory/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        var a = JsonConvert.DeserializeObject<List<ItemCategory>>(data);
                        table = a.First();
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
            return View(table);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(ItemCategory itemCategory)
        {
            
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            if (itemCategory.Description.Contains("TOPPING"))
            {
                TempData["ErrorMes"] = "Topping is exist and only have one";
                return View(itemCategory);
            }
            
            var isCombo = !Request.Form["IsCombo"].IsNullOrEmpty();
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        itemCategory.IsCombo = isCombo;
                        itemCategory.EmployeeId = emp.EmployeeId;
                        itemCategory.IsDelete = false;
                        HttpResponseMessage response = await client.PutAsJsonAsync(_apiUrl + "ItemCategory/Update", itemCategory);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("List");
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
            }
            return View();
		}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(_apiUrl + "ItemCategory/Delete/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
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
            return RedirectToAction("List");
        }
	}
}
