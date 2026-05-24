using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.API.Models;

namespace SmartLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult> GetAllAuthors()
        {
            var authors = await _context.Authors.ToArrayAsync();
            return Ok(authors);
        }

        // GET: api/authors/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();
            return Ok(author);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult> PostAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor),
                new { id = author.Id }, author);
        }

        // PUT: api/authors/1
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAuthor(
            [FromRoute] int id,
            [FromBody] Author author)
        {
            if (id != author.Id)
                return BadRequest();

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Authors.Any(a => a.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/authors/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return Ok(author);
        }
    }
}