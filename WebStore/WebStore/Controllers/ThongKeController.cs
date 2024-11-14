using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
	public class ThongKeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
