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
    public class AssetsController : ControllerBase
    {
        private readonly AssetContext _context;

        public AssetsController(AssetContext context)
        {
            _context = context;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            return await _context.Assets
                .Where(asset => asset.Userid == userId)
                .ToListAsync();
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        // PUT: api/Assets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(int id, Asset asset)
        {
            if (id != asset.Id)
            {
                return BadRequest();
            }

            _context.Entry(asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
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

        // POST: api/Assets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Asset>> PostAsset(Asset asset)
        // {
        //     _context.Assets.Add(asset);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetAsset", new { id = asset.Id }, asset);
        // }
        
        
        // POST: api/Assets
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Asset>>> InsertAssets(List<Asset> assets)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            assets.ForEach(asset =>
            {
                asset.Userid = userId;
                Console.WriteLine(asset.ToString());
                _context.Assets.Add(asset);
            });
            await _context.SaveChangesAsync();
            return await _context.Assets
                .Where(asset => asset.Userid == userId)
                .ToListAsync();
        }

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}
