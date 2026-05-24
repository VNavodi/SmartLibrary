using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.API.Models;

namespace SmartLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public CategoriesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.ToArrayAsync();
            return Ok(categories);
        }

        // GET: api/categories/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult> PostCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory),
                new { id = category.Id }, category);
        }

        // PUT: api/categories/1
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCategory(
            [FromRoute] int id,
            [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest();

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Categories.Any(c => c.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/categories/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }
    }
}