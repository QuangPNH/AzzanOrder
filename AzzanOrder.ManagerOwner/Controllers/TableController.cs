using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AzzanOrder.ManagerOwner.Models;
using System.Text;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class TableController : Controller
    {
        private readonly HttpClient _httpClient;

        public TableController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiUrl = new Config()._apiUrl;
        public async Task<IActionResult> ListAsync(int? page)
        {
            List<Table> tables = new List<Table>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Fetch tables
                    HttpResponseMessage tableRes = await client.GetAsync(_apiUrl + "Table/GetTablesByManagerId/1");
                    if (tableRes.IsSuccessStatusCode)
                    {
                        string tableData = await tableRes.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<List<Table>>(tableData) ?? new List<Table>();
                        tables = data.Where(x => x.Status != null).ToList();
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
                tables = tables,
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Table table)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        table.EmployeeId = 1;
                        table.Status = true;
                        string json = JsonConvert.SerializeObject(table);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(_apiUrl + "Table/Add", content);
                        if (response.IsSuccessStatusCode)
                        {
                            string qrCodeUrl = $"{_apiUrl}Table/GenerateQrCode/{table.Qr}/{table.EmployeeId}";
                            HttpResponseMessage response1 = await client.GetAsync(qrCodeUrl);
                            if (response1.IsSuccessStatusCode)
                            {
                                string qrCodeData = await response1.Content.ReadAsStringAsync();
                                ViewBag.QrCodeUrl = qrCodeData;
                                return View(table);
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
                    catch (HttpRequestException)
                    {
                        ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                    }
                }
            }
            return View(table);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Table table = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(_apiUrl + "Table/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        table = JsonConvert.DeserializeObject<Table>(data);
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
        public async Task<IActionResult> Update(Table table)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        table.EmployeeId = 1;
                        table.Status = true;
                        string json = JsonConvert.SerializeObject(table);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PutAsync(_apiUrl + "Table/Update", content);
                        if (response.IsSuccessStatusCode)
                        {
                            string qrCodeUrl = $"{_apiUrl}Table/GenerateQrCode/{table.Qr}/{table.EmployeeId}";
                            HttpResponseMessage response1 = await client.GetAsync(qrCodeUrl);
                            if (response1.IsSuccessStatusCode)
                            {
                                string qrCodeData = await response1.Content.ReadAsStringAsync();
                                ViewBag.QrCodeUrl = qrCodeData;
                                return View(table);
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
                    catch (HttpRequestException)
                    {
                        ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                    }
                }
            }
            return View(table);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(_apiUrl + "Table/Delete/" + id);
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
