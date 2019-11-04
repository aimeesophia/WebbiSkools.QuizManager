using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbiSkools.QuizManager.Web.Data;

namespace WebbiSkools.QuizManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuizManagerContext _context;

        public AccountController(QuizManagerContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}