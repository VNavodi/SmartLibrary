using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.API.Models;

namespace SmartLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AnalyticsController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<AnalyticsDto>> GetAnalytics()
        {
            var totalBooks = await _context.Books.SumAsync(b => b.AvailableCopies) +
                             await _context.BorrowRecords.CountAsync(br => !br.IsReturned);

            var availableBooks = await _context.Books.SumAsync(b => b.AvailableCopies);

            var membersCount = await _context.Members.CountAsync();

            var activeBorrows = await _context.BorrowRecords.CountAsync(br => !br.IsReturned);

            var analytics = new AnalyticsDto
            {
                TotalBooks = totalBooks,
                AvailableBooks = availableBooks,
                MembersCount = membersCount,
                ActiveBorrows = activeBorrows
            };

            return Ok(analytics);
        }
    }

    public class AnalyticsDto
    {
        public int TotalBooks { get; set; }
        public int AvailableBooks { get; set; }
        public int MembersCount { get; set; }
        public int ActiveBorrows { get; set; }
    }
}