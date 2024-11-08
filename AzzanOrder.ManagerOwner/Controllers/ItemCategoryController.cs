using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly string _apiUrl = "https://localhost:7183/api/";
        public async Task<IActionResult> ListAsync(int? page)
		{
            List<ItemCategory> tables = new List<ItemCategory>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage tableRes = await client.GetAsync(_apiUrl + "ItemCategory/GetByManagerId/1");
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
            int pageSize = 10;
            int pageNumber = page ?? 1;
            int maxPageNav = 10;
            int totalTables = tables.Count;
            int totalPages = (int)Math.Ceiling((double)totalTables / pageSize);

            tables = tables.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var viewModel = new Model
            {
                anIntegerUsedForCountingNumberOfPageQueuedForTheList = totalPages,
                anIntegerUsedForKnowingWhatTheCurrentPageOfTheList = pageNumber,
                thisIntegerIsUsedForKnowingTheMaxNumberOfPageNavButtonShouldBeDisplayed = maxPageNav,
                itemCategories = tables,
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Add()
		{
			return View();
		}

		// POST: Employee/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(ItemCategory itemCategory)
		{
            if (ModelState.IsValid)
			{
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        itemCategory.EmployeeId = 1;
                        itemCategory.IsDelete = false;
                        itemCategory.Employee = new Employee { };
                        string json = JsonConvert.SerializeObject(itemCategory);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(_apiUrl + "ItemCategory/Add", content);
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
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        itemCategory.EmployeeId = 1;
                        itemCategory.IsDelete = false;
                        itemCategory.Employee = new Employee { };
                        string json = JsonConvert.SerializeObject(itemCategory);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PutAsync(_apiUrl + "ItemCategory/Update", content);
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
