using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Channels.Models;

namespace Channels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly ChannelsContext _context;

        public ChannelController(ChannelsContext context)
        {
            _context = context;
        }

        // GET: api/Channel/name
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Channel>>> GetChannels(string name)
        {
            var ownerId = _context.Persons
                            .Where(person => person.Name == name)
                            .FirstOrDefault<Person>().Id;
            Console.WriteLine(ownerId);
            return await _context.Channels
                .Where(channel => channel.Ownerid == ownerId)
                .ToListAsync();
        }
        
        // GET: api/Channel/mine/channels
        [HttpGet("subscribed")]
        public async Task<ActionResult<IEnumerable<Channel>>> GetMyChannels()
        {
            var name = HttpContext.Session.GetString("username");
            Console.WriteLine("My name is:" + name);
            return await _context.Channels.Where(channel =>
                channel.Subscribers.Contains(name)).ToListAsync();
        }
        
        // PUT: api/Channel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChannel(int id, Channel channel)
        {
            if (id != channel.Id)
            {
                return BadRequest();
            }

            _context.Entry(channel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChannelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Channel/subscribe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("subscribe")]
        public async Task<ActionResult<Channel>> PostChannel([FromForm]string name)
        {
            var userName = HttpContext.Session.GetString("username");
            var channel = _context.Channels.FirstOrDefault(channel => channel.Name == name);
            string DateTime = System.DateTime.Now.ToString("dd/MM/yyyy"); 
            if (!channel.Subscribers.Contains(userName))
            {
                channel.Subscribers += " " + userName + ": " + DateTime + ";";
            }
            else
            {
                string pattern = $"(?<={userName}: )(.*?)(?=;)";
                var subscribers = Regex.Replace(channel.Subscribers, pattern, DateTime);
                channel.Subscribers = subscribers;
               
            }

            Console.WriteLine(channel.Subscribers);
            await _context.SaveChangesAsync();
            return channel;
        }

        // DELETE: api/Channel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannel(int id)
        {
            var channel = await _context.Channels.FindAsync(id);
            if (channel == null)
            {
                return NotFound();
            }

            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChannelExists(int id)
        {
            return _context.Channels.Any(e => e.Id == id);
        }
    }
}
