using Microsoft.AspNetCore.Mvc;

namespace AzzanOrder.ManagerOwner.Controllers
{
	public class FeedbackController : Controller
	{
		public IActionResult List()
		{
			return View();
		}
	}
}
