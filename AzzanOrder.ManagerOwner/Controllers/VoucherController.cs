using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AzzanOrder.ManagerOwner.Controllers
{
	public class VoucherController : Controller
	{
        private readonly HttpClient _httpClient;

        public VoucherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiUrl = "https://localhost:7183/api/";

        public async Task<IActionResult> ListAsync(int? page)
        {
            List<VoucherDetail> vouchers = new List<VoucherDetail>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
					var url = 1 == 1 ? _apiUrl + "Vouchers" : _apiUrl + "Vouchers?id=1";
                    HttpResponseMessage voucherRes = await client.GetAsync(url);
                    if (voucherRes.IsSuccessStatusCode)
                    {
                        string voucherData = await voucherRes.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<List<VoucherDetail>>(voucherData) ?? new List<VoucherDetail>();
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
            return View(vouchers);
        }

        public IActionResult Add()
		{
			return View();
		}

		// POST: Employee/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddPost()
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("List");
			}

			// If model validation fails, redisplay the form with validation messages
			return View();
		}


		public IActionResult Update()
		{
			return View();
		}

		// POST: Employee/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdatePost()
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("List");
			}

			// If model validation fails, redisplay the form with validation messages
			return View();
		}
		public IActionResult Delete()
		{
			return RedirectToAction("List", "Voucher");
		}
	}
}
