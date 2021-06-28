using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authors.Models;

namespace Authors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly AuthorContext _context;

        public DocumentController(AuthorContext context)
        {
            _context = context;
        }

        // GET: api/Document
        [HttpGet]
        public ActionResult<String> GetDocuments()
        {
            String name = HttpContext.Session.GetString("username");
            var author = _context.Authors
                .Where(a => a.Name == name)
                .FirstOrDefault();
            Console.WriteLine(author.DocumentList + " " + author.MovieList);
            String DocumentList = author.DocumentList;
            String MovieList = author.MovieList;
            var docNames = DocumentList.Split(",");
            var movieNames = MovieList.Split(",");
            String res = "";
            for(int i = 0; i < docNames.Length - 1; i++) {
                var doc = _context.Documents
                    .Where(d => d.Name == docNames[i])
                    .FirstOrDefault();
                Console.WriteLine(doc.Name);
                res += "Document: " + doc.Name + " " + doc.Contents + " ";
                var movie = _context.Movies
                    .Where(m => m.Title == movieNames[i])
                    .FirstOrDefault();
                Console.WriteLine(movie.Title);
                res += "Movie: " + movie.Title + " " + movie.Duration + " ";
                res += "<br>";
            }

            Console.WriteLine(res);
            return res;
        }
        
        // GET: api/Document
        [HttpGet("most")]
        public ActionResult<String> GetMost()
        {
            var documents = _context.Documents.ToList();
            var authors = _context.Authors.ToList();
            int max = 0;
            String bestDoc = "";
            foreach (var doc in documents)
            {
                int count = 0;
                foreach (var author in authors)
                {
                    if (author.DocumentList.Contains(doc.Name))
                    {
                        count++;
                    }
                }
                if (count > max)
                {
                    max = count;
                    bestDoc = doc.Name;
                }
            }
            return bestDoc;
        }

        // GET: api/Document/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // PUT: api/Document/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
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

        // POST: api/Document
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument([FromForm]String name, [FromForm]String contents)
        {
            Document d = new Document();
            d.Name = name;
            d.Contents = contents;
            _context.Documents.Add(d);
            await _context.SaveChangesAsync();

            return d;
        }

        // DELETE: api/Document/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
