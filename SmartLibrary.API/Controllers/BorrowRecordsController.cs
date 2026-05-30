using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.API.Models;

namespace SmartLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowRecordsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowRecordsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/borrowrecords
        [HttpGet]
        public async Task<ActionResult> GetAllBorrowRecords()
        {
            var records = await _context.BorrowRecords
                .Include(br => br.Member)
                .Include(br => br.Book)
                .ToArrayAsync();
            return Ok(records);
        }

        // GET: api/borrowrecords/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBorrowRecord(int id)
        {
            var record = await _context.BorrowRecords
                .Include(br => br.Member)
                .Include(br => br.Book)
                .FirstOrDefaultAsync(br => br.Id == id);
            if (record == null)
                return NotFound();
            return Ok(record);
        }

        // GET: api/borrowrecords/overdue
        [HttpGet("overdue")]
        public async Task<ActionResult> GetOverdueRecords()
        {
            var overdueRecords = await _context.BorrowRecords
                .Include(br => br.Member)
                .Include(br => br.Book)
                .Where(br => br.IsReturned == false &&
                             br.DueDate < DateTime.Now)
                .ToArrayAsync();

            return Ok(overdueRecords);
        }

        // POST: api/borrowrecords/borrow
        [HttpPost("borrow")]
        public async Task<ActionResult> BorrowBook(
            [FromBody] BorrowRecord borrowRecord)
        {
            // is Book exist
            var book = await _context.Books.FindAsync(borrowRecord.BookId);
            if (book == null)
                return NotFound("Book not found");

            // is Book available
            if (!book.IsAvailable || book.AvailableCopies <= 0)
                return BadRequest("Book is not available");

            // is Member exist 
            var member = await _context.Members
                .FindAsync(borrowRecord.MemberId);
            if (member == null)
                return NotFound("Member not found");

            // BorrowRecord setup
            borrowRecord.BorrowDate = DateTime.Now;
            borrowRecord.DueDate = DateTime.Now.AddDays(14);
            borrowRecord.IsReturned = false;

            // reduce Book copies
            book.AvailableCopies--;
            if (book.AvailableCopies == 0)
                book.IsAvailable = false;

            _context.BorrowRecords.Add(borrowRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrowRecord),
                new { id = borrowRecord.Id }, borrowRecord);
        }

        // PUT: api/borrowrecords/return/1
        [HttpPut("return/{id}")]
        public async Task<ActionResult> ReturnBook(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null)
                return NotFound("Borrow record not found");

            if (record.IsReturned)
                return BadRequest("Book already returned");

            // increase Book copies 
            var book = await _context.Books.FindAsync(record.BookId);
            if (book != null)
            {
                book.AvailableCopies++;
                book.IsAvailable = true;
            }

            // Record update 
            record.ReturnDate = DateTime.Now;
            record.IsReturned = true;

            await _context.SaveChangesAsync();

            return Ok(record);
        }

        // DELETE: api/borrowrecords/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBorrowRecord(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null)
                return NotFound();

            _context.BorrowRecords.Remove(record);
            await _context.SaveChangesAsync();

            return Ok(record);
        }
    }
}