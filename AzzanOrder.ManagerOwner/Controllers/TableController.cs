using Microsoft.AspNetCore.Mvc;

namespace AzzanOrder.ManagerOwner.Controllers
{
	public class TableController : Controller
	{
		public IActionResult List()
		{
			return View();
		}

		public IActionResult Add()
		{
			return View();
		}

		public IActionResult Update()
		{
			return View();
		}

		public IActionResult Delete()
		{
			return RedirectToAction("List", "Table");
		}
	}
}
