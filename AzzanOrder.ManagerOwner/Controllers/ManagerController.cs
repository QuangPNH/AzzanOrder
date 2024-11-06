using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult List()
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
                return RedirectToAction("List");
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
                return RedirectToAction("Update");
            }

            // If model validation fails, redisplay the form with validation messages
            return View();
        }

        public IActionResult MenuList()
        {
            return View();
        }

        public IActionResult MenuItemList()
        {
            return View();
        }

        public IActionResult MenuItemDetail()
        {
            return View();
        }


    }
}
