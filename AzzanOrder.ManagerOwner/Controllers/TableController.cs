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

        private readonly string _apiUrl = "https://localhost:7183/api/";
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
                        tables = JsonConvert.DeserializeObject<List<Table>>(tableData) ?? new List<Table>();
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
            return View(tables);
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
                            var createdTable = JsonConvert.DeserializeObject<Table>(await response.Content.ReadAsStringAsync());
                            string qrCodeUrl = $"{_apiUrl}Table/GenerateQrCode/{createdTable.Qr}/{createdTable.EmployeeId}";
                            ViewBag.QrCodeUrl = qrCodeUrl;
                            return View(table);
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
    }
}
