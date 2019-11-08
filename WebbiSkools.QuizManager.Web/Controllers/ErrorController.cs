using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace WebbiSkools.QuizManager.Web.Controllers
{
    [ExcludeFromCodeCoverage]
    public class ErrorController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}