using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.API.Models;

namespace SmartLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly LibraryContext _context;

        public MembersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/members
        [HttpGet]
        public async Task<ActionResult> GetAllMembers()
        {
            var members = await _context.Members.ToArrayAsync();
            return Ok(members);
        }

        // GET: api/members/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                return NotFound();
            return Ok(member);
        }

        // GET: api/members/1/borrowhistory
        [HttpGet("{id}/borrowhistory")]
        public async Task<ActionResult> GetMemberBorrowHistory(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                return NotFound();

            var history = await _context.BorrowRecords
                .Where(br => br.MemberId == id)
                .ToArrayAsync();

            return Ok(history);
        }

        // GET: api/members/1/activeborrows
        [HttpGet("{id}/activeborrows")]
        public async Task<ActionResult> GetActiveBorrows(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                return NotFound();

            var activeborrows = await _context.BorrowRecords
                .Where(br => br.MemberId == id && br.IsReturned == false)
                .ToArrayAsync();

            return Ok(activeborrows);
        }

        // POST: api/members
        [HttpPost]
        public async Task<ActionResult> PostMember([FromBody] Member member)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            member.MembershipDate = DateTime.Now;
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMember),
                new { id = member.Id }, member);
        }

        // PUT: api/members/1
        [HttpPut("{id}")]
        public async Task<ActionResult> PutMember(
            [FromRoute] int id,
            [FromBody] Member member)
        {
            if (id != member.Id)
                return BadRequest();

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Members.Any(m => m.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/members/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                return NotFound();

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return Ok(member);
        }
    }
}