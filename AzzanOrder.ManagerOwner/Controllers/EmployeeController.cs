using Microsoft.AspNetCore.Mvc;

namespace AzzanOrder.ManagerOwner.Controllers
{
	public class EmployeeController : Controller
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
			return RedirectToAction("List", "Employee");
		}

	}
}
