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
            List<VoucherDetail> voucherDetails = new List<VoucherDetail>();
            List<Voucher> vouchers = new List<Voucher>();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //HttpContext.Request.Cookies.TryGetValue("TempLoginInfo", out string loginInfoJson);


                    var url = emp != null ? _apiUrl + $"VoucherDetail/ListVoucherDetail?employeeId={emp.EmployeeId}" : _apiUrl + "VoucherDetail/ListVoucherDetail";
                    HttpResponseMessage voucherDetailRes = await client.GetAsync(url);
                    if (voucherDetailRes.IsSuccessStatusCode)
                    {
                        string voucherDetailData = await voucherDetailRes.Content.ReadAsStringAsync();
                        voucherDetails = JsonConvert.DeserializeObject<List<VoucherDetail>>(voucherDetailData);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                    var url1 = _apiUrl + "Vouchers";
                    HttpResponseMessage voucherRes = await client.GetAsync(url1);
                    if (voucherRes.IsSuccessStatusCode)
                    {
                        string voucherData = await voucherRes.Content.ReadAsStringAsync();
                        vouchers = JsonConvert.DeserializeObject<List<Voucher>>(voucherData);
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
                voucherDetails = voucherDetails,
                vouchers = vouchers
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
            List<Voucher> vouchers = new List<Voucher>();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //HttpContext.Request.Cookies.TryGetValue("TempLoginInfo", out string loginInfoJson);
                    var url = emp != null ? _apiUrl + $"ItemCategory?id={emp.EmployeeId}" : _apiUrl + "ItemCategory";
                    HttpResponseMessage itemCats = await client.GetAsync(url);
                    if (itemCats.IsSuccessStatusCode)
                    {
                        string itemCategoryList = await itemCats.Content.ReadAsStringAsync();
                        itemCategories = JsonConvert.DeserializeObject<List<ItemCategory>>(itemCategoryList);
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
                //,
                //vouchers = vouchers
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(VoucherDetail voucherDetail)
        {
            //if (ModelState.IsValid)
            //{
            //    return RedirectToAction("List");
            //}
            // Lưu lại voucherDetail và vòng lặp để lưu lại từng voucher
            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            voucherDetail.EmployeeId = emp.EmployeeId;
            //List<Voucher> voucherlist = new List<Voucher>();
            var selectedCategories = Request.Form["SelectedCategories"].Select(int.Parse).ToList();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(_apiUrl + "VoucherDetail/Add/", voucherDetail))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                        voucherDetail = JsonConvert.DeserializeObject<VoucherDetail>(message);

                    }
                }
                foreach (var categoryId in selectedCategories)
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(_apiUrl + "Vouchers/Add/", new Voucher() { ItemCategoryId = categoryId, VoucherDetailId = voucherDetail.VoucherDetailId, IsActive = true }))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string message = await res.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            // If model validation fails, redisplay the form with validation messages
            return RedirectToAction("List", "Voucher");
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
            VoucherDetail vd = new VoucherDetail();
            Employee emp = new Employee();
            List<ItemCategory> itemCategories = new List<ItemCategory>();
            
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = emp != null ? _apiUrl + $"ItemCategory?id={emp.EmployeeId}" : _apiUrl + "ItemCategory";
                    HttpResponseMessage itemCats = await client.GetAsync(url);
                    if (itemCats.IsSuccessStatusCode)
                    {
                        string itemCategoryList = await itemCats.Content.ReadAsStringAsync();
                        itemCategories = JsonConvert.DeserializeObject<List<ItemCategory>>(itemCategoryList);
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
                    HttpResponseMessage res = await client.GetAsync(_apiUrl + "VoucherDetail/" + id);
                    if (res.IsSuccessStatusCode)
                    {
                        string data = await res.Content.ReadAsStringAsync();
                        vd = JsonConvert.DeserializeObject<VoucherDetail>(data);
                    }
                    else
                    {
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
            Model model = new Model
            {
                itemCategories = itemCategories,
                voucherDetail = vd
                
                //,
                //vouchers = vouchers
            };
            return View(model);
        }


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
