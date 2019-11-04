using Microsoft.AspNetCore.Mvc;

namespace WebbiSkools.QuizManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
