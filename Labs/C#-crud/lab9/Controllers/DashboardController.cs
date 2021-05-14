using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab9.Controllers
{
    public class DashboardController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("loggedin") != "yes")
                return Redirect("/Account/Login");
            return View();
        }
    }
}
