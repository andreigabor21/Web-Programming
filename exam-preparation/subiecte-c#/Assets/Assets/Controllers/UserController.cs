using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assets.Models;

namespace Assets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AssetContext _context;

        public UserController(AssetContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        public ActionResult Login([FromForm]String username, [FromForm]String password)
        {
            Console.WriteLine("In Login");
            var foundUser = _context.Users.Where(u => u.Username == username && u.Password == password)
                .FirstOrDefault();
            Console.WriteLine(foundUser.ToString());
            HttpContext.Session.SetInt32("userId", foundUser.Id);
            
            return Redirect("/index.html");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
