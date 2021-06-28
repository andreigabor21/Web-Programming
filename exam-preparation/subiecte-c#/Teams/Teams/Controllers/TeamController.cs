using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.Models;

namespace Teams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamsContext _context;

        public TeamController(TeamsContext context)
        {
            _context = context;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }
        
        [HttpGet("MyTeams")]
        public async Task<ActionResult<IEnumerable<Team>>> GetMyTeams()
        {
            var myName = HttpContext.Session.GetString("username");
            Console.WriteLine(myName);
            return await _context.Teams
                .Where(t => t.Members.Contains(myName))
                .ToListAsync();
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Team
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Team>>> PostTeam([FromForm]String teams, [FromForm]String player)
        {
            Console.WriteLine(teams);
            Console.WriteLine(player);
            
            string[] words = teams.Split(',');
            foreach (var team in words)
            {
                System.Console.WriteLine(team);
                var teamFound = _context.Teams
                    .FirstOrDefault<Team>(t => t.Name == team);
                var playerFound = _context.Players
                    .FirstOrDefault<Player>(p => p.Name == player);
                if (playerFound == null)
                {
                    Player newPlayer = new Player();
                    newPlayer.Name = player;
                    _context.Players.Add(newPlayer);
                    await _context.SaveChangesAsync();
                }
                teamFound.Members += player + ";";
                await _context.SaveChangesAsync();
            }

            return await _context.Teams.ToListAsync();
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
