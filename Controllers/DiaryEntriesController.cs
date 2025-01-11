using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicHealthRecord.Data;
using ElectronicHealthRecord.Models;
using ElectronicHealthRecord.DTOs;

namespace ElectronicHealthRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiaryEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DiaryEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryEntryDto>>> GetDiaryEntries()
        {
            var diaryEntries = await _context.DiaryEntries
                .Select(e => new DiaryEntryDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Text = e.Text,
                    Date = e.Date,
                    Attachments = e.Attachments,
                    FolderId = e.FolderId,
                    UserId = e.UserId
                })
                .ToListAsync();

            return Ok(diaryEntries);
        }

        // GET: api/DiaryEntries/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntryDto>> GetDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries
                .Where(e => e.Id == id)
                .Select(e => new DiaryEntryDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Text = e.Text,
                    Date = e.Date,
                    Attachments = e.Attachments,
                    FolderId = e.FolderId,
                    UserId = e.UserId
                })
                .FirstOrDefaultAsync();

            if (diaryEntry == null)
            {
                return NotFound(new { Message = $"Diary entry with ID {id} not found." });
            }

            return Ok(diaryEntry);
        }

        // POST: api/DiaryEntries
        [HttpPost]
        public async Task<ActionResult<DiaryEntryDto>> CreateDiaryEntry(CreateDiaryEntryDto createDto)
        {
            var diaryEntry = new DiaryEntry
            {
                Title = createDto.Title,
                Text = createDto.Text,
                Date = createDto.Date,
                Attachments = createDto.Attachments,
                FolderId = createDto.FolderId,
                UserId = createDto.UserId
            };

            _context.DiaryEntries.Add(diaryEntry);
            await _context.SaveChangesAsync();

            var diaryEntryDto = new DiaryEntryDto
            {
                Id = diaryEntry.Id,
                Title = diaryEntry.Title,
                Text = diaryEntry.Text,
                Date = diaryEntry.Date,
                Attachments = diaryEntry.Attachments,
                FolderId = diaryEntry.FolderId,
                UserId = diaryEntry.UserId
            };

            return CreatedAtAction(nameof(GetDiaryEntry), new { id = diaryEntryDto.Id }, diaryEntryDto);
        }

        // PUT: api/DiaryEntries/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiaryEntry(int id, UpdateDiaryEntryDto updateDto)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);

            if (diaryEntry == null)
            {
                return NotFound(new { Message = $"Diary entry with ID {id} not found." });
            }

            diaryEntry.Title = updateDto.Title;
            diaryEntry.Text = updateDto.Text;
            diaryEntry.Date = updateDto.Date;
            diaryEntry.Attachments = updateDto.Attachments;
            diaryEntry.FolderId = updateDto.FolderId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the diary entry." });
            }

            return NoContent();
        }

        // DELETE: api/DiaryEntries/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);

            if (diaryEntry == null)
            {
                return NotFound(new { Message = $"Diary entry with ID {id} not found." });
            }

            _context.DiaryEntries.Remove(diaryEntry);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Diary entry with ID {id} deleted successfully." });
        }
    }
}
