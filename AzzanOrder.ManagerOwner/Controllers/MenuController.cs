using Microsoft.AspNetCore.Mvc;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("List");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }

        public IActionResult ListItem()
        {
            return View();
        }
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItemPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ListItem");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }
        public IActionResult EditItem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItemPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ListItem");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }
    }
}
