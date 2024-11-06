using AzzanOrder.ManagerOwner.Models; // Update this line
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Reflection;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class PromotionController : Controller
    {
        public async Task<IActionResult> ListRunning()
        {
            dynamic promotions = new ExpandoObject();
            promotions.Logo = new Promotion();
            promotions.BackgroundColor = "";
            promotions.Carousel = new List<Promotion>();
            promotions.Banner = new List<Promotion>();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var logoResponse = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetByDescription/logo");
                    promotions.Logo = JsonConvert.DeserializeObject<Promotion>(logoResponse);
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                }

                try
                {
                    var backgroundColorResponse = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetByDescription/color");
                    var data = JsonConvert.DeserializeObject<Promotion>(backgroundColorResponse);
                    promotions.BackgroundColor = data.Description.Split('/')[1];
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                }

                try
                {
                    var carouselResponse = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetByDescription/carousel");
                    promotions.Carousel = JsonConvert.DeserializeObject<List<Promotion>>(carouselResponse);
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                }

                try
                {
                    var bannerResponse = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetByDescription/banner");
                    promotions.Banner = JsonConvert.DeserializeObject<List<Promotion>>(bannerResponse);
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                }
            }
            return View(promotions);
        }

        public async Task<IActionResult> ListAll()
        {
            dynamic promotions = new ExpandoObject();
            promotions.Logo = new Promotion();
            promotions.BackgroundColor = "";
            promotions.Carousel = new List<Promotion>();
            promotions.Banner = new List<Promotion>();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var logoResponse = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetByDescription/logo");
                    promotions.Logo = JsonConvert.DeserializeObject<Promotion>(logoResponse);
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                }

                try
                {
                    var backgroundColorResponse = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetByDescription/color");
                    var data = JsonConvert.DeserializeObject<Promotion>(backgroundColorResponse);
                    promotions.BackgroundColor = data.Description.Split('/')[1];
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                }

                try
                {
                    var carouselResponse = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetByDescription/carousel");
                    promotions.Carousel = JsonConvert.DeserializeObject<List<Promotion>>(carouselResponse);
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                }

                try
                {
                    var bannerResponse = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetByDescription/banner");
                    promotions.Banner = JsonConvert.DeserializeObject<List<Promotion>>(bannerResponse);
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                }
            }
            return View(promotions);
        }

        public IActionResult Add()
        {
            return View();
        }

        // POST: Employee/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Running");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        // POST: Employee/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ListRunning");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveStatus([FromBody] object data)
        {
            // Handle the save status logic here
            // For example, update the database with the new status

            return Ok();
        }
    }
}
