using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Topics.Models;

namespace Topics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly TopicsContext _context;

        public PostController(TopicsContext context)
        {
            _context = context;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }
        
        [HttpGet("new")]
        public ActionResult<string> Notify()
        {
            int localLength = _context.Posts.ToList().Count;
            var length = HttpContext.Session.GetInt32("length");
            if (localLength > length)
            {
                HttpContext.Session.SetInt32("length", localLength);
                string userName = HttpContext.Session.GetString("username");
                var post = _context.Posts.OrderBy(post => post.Id).Last();
                Console.WriteLine(post);
                if (post.User != userName)
                {
                    return post.ToString();
                }
            }

            return "-";
        }
        
        // PUT: api/Post/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post data)
        {
            Console.WriteLine(data.Text);
            Console.WriteLine(data.TopicId);
            
            data.User = HttpContext.Session.GetString("username");
            data.Date = DateTime.Today.Day;

            _context.Entry(data).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostPost([FromForm]String username)
        {
            Console.WriteLine("In Login " + username);
            HttpContext.Session.SetString("username", username);

            var length = _context.Posts.ToList().Count;
            HttpContext.Session.SetInt32("length", length);
            
            return Redirect("/index.html");
        }
        
        // POST: api/Post/add
        [HttpPost("add")]
        public async Task<ActionResult<IEnumerable<Post>>> PostPost([FromForm]String topicName, [FromForm]String text)
        {
            Console.WriteLine(topicName + " " + text);
            var topic = _context.Topics
                .FirstOrDefault<Topic>(t => t.TopicName == topicName);
            if (topic == null)
            {
                Console.WriteLine("is null");
                Topic newTopic = new Topic();
                newTopic.TopicName = topicName;
                _context.Topics.Add(newTopic);
                await _context.SaveChangesAsync();
            }
            
            Console.WriteLine(topic);
                Post post = new Post();
                var topicId = _context.Topics
                    .FirstOrDefault<Topic>(t => t.TopicName == topicName).Id;
                post.User = HttpContext.Session.GetString("username");
                post.Date = DateTime.Today.Day;
                post.TopicId = topicId;
                post.Text = text;
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                
                return await _context.Posts.ToListAsync();
        }
        
    }
}
