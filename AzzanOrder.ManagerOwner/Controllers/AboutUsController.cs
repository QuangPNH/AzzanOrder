using Microsoft.AspNetCore.Mvc;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Manage()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Manage");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }
    }
}