using AzzanOrder.ManagerOwner.Models; // Update this line
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class PromotionController : Controller
    {
        public async Task<IActionResult> ListRunning()
        {
            List<Promotion> promotions = new List<Promotion>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync("https://localhost:7183/api/Promotions/GetByDescription/carousel");
                promotions = JsonConvert.DeserializeObject<List<Promotion>>(response) ?? new List<Promotion>();
            }

            ViewBag.CurrentIndex = 0; // Initialize the current index
            return View(promotions);
        }

        public IActionResult ListAll()
        {
            return View();
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

        public IActionResult Delete()
        {
            return RedirectToAction("ListAll", "Promotion");
        }
    }
}
