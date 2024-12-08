using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AzzanOrder.ManagerOwner.Controllers
{
	public class FeedbackController : Controller
	{
        private readonly HttpClient _httpClient;
        public FeedbackController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private readonly string _apiUrl = new Config()._apiUrl;
        public async Task<IActionResult> ListAsync()
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
            List<Feedback> feedBackDatas = new List<Feedback>();
            List<Member> members = new List<Member>();
            var menuItemCounts = 0;

            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = _apiUrl + "Feedback";
                    HttpResponseMessage feedBackRes = await client.GetAsync(url);
                    if (feedBackRes.IsSuccessStatusCode)
                    {
                        string feedBackData = await feedBackRes.Content.ReadAsStringAsync();
                        feedBackDatas = JsonConvert.DeserializeObject<List<Feedback>>(feedBackData);
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
                    var url = _apiUrl + "Member";
                    HttpResponseMessage memberRes = await client.GetAsync(url);
                    if (memberRes.IsSuccessStatusCode)
                    {
                        string memberData = await memberRes.Content.ReadAsStringAsync();
                        members = JsonConvert.DeserializeObject<List<Member>>(memberData);
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
                feedbacks = feedBackDatas,
                members = members,
                employee = emp
            };
            return View(model);
		}
	}
}
