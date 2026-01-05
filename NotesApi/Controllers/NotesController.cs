using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Models;
using System.Security.Claims;

namespace NotesApi.Controllers
{
    [Route("notes")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public NotesController(LibraryContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return idClaim != null ? int.Parse(idClaim.Value) : 0;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            var userId = GetUserId();
            return await _context.Notes
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var userId = GetUserId();
            var note = await _context.Notes.FindAsync(id);

            if (note == null) return NotFound();

            if (note.UserId != userId) return Forbid();

            return note;
        }

        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(NoteDto dto)
        {
            var userId = GetUserId();

            var note = new Note
            {
                Content = dto.Content,
                UserId = userId
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, NoteDto dto)
        {
            var userId = GetUserId();

            var note = await _context.Notes.FindAsync(id);

            if (note == null) return NotFound();

            // Blokada edycji cudzych notatek
            if (note.UserId != userId) return Forbid();

            note.Content = dto.Content;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var userId = GetUserId();
            var note = await _context.Notes.FindAsync(id);

            if (note == null) return NotFound();

            if (note.UserId != userId) return Forbid();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}