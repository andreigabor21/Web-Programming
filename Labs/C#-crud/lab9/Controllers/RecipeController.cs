using Ganss.XSS;
using lab9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace lab9.Controllers
{
    public class RecipeController : Controller
    {
        private String con = "Data Source = 'DESKTOP-FGTU247\\SQLEXPRESS';" +
                                    "Initial Catalog = 'Web';" +
                                    "Integrated Security = true;";
        HtmlSanitizer sanitizer = new HtmlSanitizer();


        // GET: RecipeController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("loggedin") != "yes")
                return Redirect("/Account/Login");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Recipe recipe)
        {
            if (HttpContext.Session.GetString("loggedin") != "yes")
                return Redirect("/Account/Login");

            var sql = "INSERT INTO recipes(name, author, type, description) VALUES ('" + recipe.name + "', '" + recipe.author + "', '" + recipe.type + "', '" + recipe.description + "')";

            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    SqlDataReader dr = command.ExecuteReader();
                }
            }

            return Redirect("/Dashboard");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("loggedin") != "yes")
                return Redirect("/Account/Login");

            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    List<Recipe> results = new List<Recipe>();

                    var query = "SELECT * FROM recipes";
                    command.CommandText = query;
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        results.Add(new Recipe 
                        {
                            id = int.Parse(dr.GetValue(0).ToString()), 
                            name = dr.GetValue(1).ToString(), 
                            author = dr.GetValue(2).ToString(), 
                            type = dr.GetValue(3).ToString(), 
                            description = dr.GetValue(4).ToString(), 
                            }
                        );
                    }

                    return View(results);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteSpecific(int id)
        {
            if (HttpContext.Session.GetString("loggedin") != "yes")
                return Redirect("/Account/Login");

            var sql = "DELETE FROM recipes where id = " + id;

            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }

            return Redirect("/Dashboard");
        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("loggedin") != "yes")
                return Redirect("/Account/Login");

            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    var query = "SELECT * FROM recipes";
                    List<Recipe> results = new List<Recipe>();

                    command.CommandText = query;
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        results.Add(new Recipe
                        {
                            id = int.Parse(dr.GetValue(0).ToString()),
                            name = dr.GetValue(1).ToString(),
                            author = dr.GetValue(2).ToString(),
                            type = dr.GetValue(3).ToString(),
                            description = dr.GetValue(4).ToString(),
                        }
                        );
                    }

                    return View(results);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult UpdateSpecific(int id)
        {
            if (HttpContext.Session.GetString("loggedin") != "yes")
                return Redirect("/Account/Login");

            var sql = "SELECT * FROM recipes WHERE id = " + id;
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        Recipe recipe = new Recipe
                        {
                            id = int.Parse(dr.GetValue(0).ToString()),
                            name = dr.GetValue(1).ToString(),
                            author = dr.GetValue(2).ToString(),
                            type = dr.GetValue(3).ToString(),
                            description = dr.GetValue(4).ToString(),
                        };
                        return View(recipe);
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateSpecific(Recipe recipe)
        {
            if (HttpContext.Session.GetString("loggedin") != "yes")
                return Redirect("/Account/Login");

            recipe.name = sanitizer.Sanitize(recipe.name);
            recipe.author = sanitizer.Sanitize(recipe.author);
            recipe.type = sanitizer.Sanitize(recipe.type);
            recipe.description = sanitizer.Sanitize(recipe.description);

            var sql = "Update recipes SET name='" + recipe.name + "', author='" + recipe.author + "', type='" + recipe.type + "', description='" + recipe.description + "' WHERE id=" + recipe.id;

            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    SqlDataReader dr = command.ExecuteReader();
                }
            }

            return Redirect("/Dashboard");
        }


        [HttpGet]
        public IActionResult Browse(int id)
        {
            if (HttpContext.Session.GetString("loggedin") != "yes")
                return Redirect("/Account/Login");

            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    var query = "SELECT * FROM recipes";
                    List<Recipe> results = new List<Recipe>();

                    command.CommandText = query;
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        results.Add(new Recipe
                        {
                            id = int.Parse(dr.GetValue(0).ToString()),
                            name = dr.GetValue(1).ToString(),
                            author = dr.GetValue(2).ToString(),
                            type = dr.GetValue(3).ToString(),
                            description = dr.GetValue(4).ToString(),
                        });
                    }
                    return View(results);
                }
            }
            return View();
        }

        [HttpGet]
        public String BrowseSpecific(String type, int id)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    var query = "SELECT * FROM recipes WHERE type LIKE '%" + type + "%'";
                    List<Recipe> results = new List<Recipe>();

                    command.CommandText = query;
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        results.Add(new Recipe 
                        {
                            id = int.Parse(dr.GetValue(0).ToString()),
                            name = dr.GetValue(1).ToString(),
                            author = dr.GetValue(2).ToString(),
                            type = dr.GetValue(3).ToString(),
                            description = dr.GetValue(4).ToString(),
                        });
                    }
                    Console.WriteLine(results);
                    var toReturn = Newtonsoft.Json.JsonConvert.SerializeObject(results);
                    Console.WriteLine(toReturn);

                    return Newtonsoft.Json.JsonConvert.SerializeObject(results);
                }
            }
        }
    }


}
