using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.API.Models;

namespace SmartLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult> GetAllBooks()
        {
            var books = await _context.Books.ToArrayAsync();
            return Ok(books);
        }

        // GET: api/books/available
        [HttpGet("available")]
        public async Task<ActionResult> GetAvailableBooks()
        {
            var books = await _context.Books
                .Where(b => b.IsAvailable == true)
                .ToArrayAsync();
            return Ok(books);
        }

        // GET: api/books/search?title=harry
        [HttpGet("search")]
        public async Task<ActionResult> SearchBooks(
            [FromQuery] string? title,
            [FromQuery] string? isbn)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrEmpty(isbn))
                query = query.Where(b => b.ISBN == isbn);

            var books = await query.ToArrayAsync();
            return Ok(books);
        }

        // GET: api/books/bycategory/1
        [HttpGet("bycategory/{categoryId}")]
        public async Task<ActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await _context.Books
                .Where(b => b.CategoryId == categoryId)
                .ToArrayAsync();
            return Ok(books);
        }

        // GET: api/books/byauthor/1
        [HttpGet("byauthor/{authorId}")]
        public async Task<ActionResult> GetBooksByAuthor(int authorId)
        {
            var books = await _context.Books
                .Where(b => b.AuthorId == authorId)
                .ToArrayAsync();
            return Ok(books);
        }

        // GET: api/books/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult> PostBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook),
                new { id = book.Id }, book);
        }

        // PUT: api/books/1
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBook(
            [FromRoute] int id,
            [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest();

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(b => b.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/books/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        // POST: api/books/delete?ids=1&ids=2
        [HttpPost("delete")]
        public async Task<ActionResult> DeleteMultipleBooks(
            [FromQuery] int[] ids)
        {
            var books = new List<Book>();

            foreach (var id in ids)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                    return NotFound();
                books.Add(book);
            }

            _context.Books.RemoveRange(books);
            await _context.SaveChangesAsync();

            return Ok(books);
        }
    }
}