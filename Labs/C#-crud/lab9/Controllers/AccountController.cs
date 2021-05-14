using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using lab9.Models;
using System.Data;
using System.Data.SqlClient;


namespace lab9.Controllers
{
    public class AccountController : Controller
    {

        private String con = "Data Source = 'DESKTOP-FGTU247\\SQLEXPRESS';" +
                                    "Initial Catalog = 'Web';" +
                                    "Integrated Security = true;";

        // GET
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("loggedin") == "yes")
                return Redirect("/Dashboard");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("loggedin", "no");
            return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM users WHERE username = @username AND password = @password";
                    command.Prepare();
                    command.Parameters.AddWithValue("@username", acc.Username);
                    command.Parameters.AddWithValue("@password", acc.Password);

                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        HttpContext.Session.SetString("loggedin", "yes");
                        ViewBag.username = acc.Username;
                        return Redirect("/Dashboard");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
        }
    }
}
