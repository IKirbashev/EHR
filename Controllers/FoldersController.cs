using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicHealthRecord.Data;
using ElectronicHealthRecord.Models;
using ElectronicHealthRecord.DTOs;

namespace ElectronicHealthRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FoldersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Folders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolderDto>>> GetFolders()
        {
            var folders = await _context.Folders
                .Include(f => f.DiaryEntries)
                .Select(f => new FolderDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    UserId = f.UserId,
                    DiaryEntryIds = f.DiaryEntries.Select(d => d.Id)
                })
                .ToListAsync();

            return Ok(folders);
        }

        // GET: api/Folders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FolderDto>> GetFolder(int id)
        {
            var folder = await _context.Folders
                .Include(f => f.DiaryEntries)
                .Where(f => f.Id == id)
                .Select(f => new FolderDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    UserId = f.UserId,
                    DiaryEntryIds = f.DiaryEntries.Select(d => d.Id)
                })
                .FirstOrDefaultAsync();

            if (folder == null)
            {
                return NotFound(new { Message = $"Folder with ID {id} not found." });
            }

            return Ok(folder);
        }

        // POST: api/Folders
        [HttpPost]
        public async Task<ActionResult<FolderDto>> CreateFolder(CreateFolderDto createDto)
        {
            var folder = new Folder
            {
                Name = createDto.Name,
                UserId = createDto.UserId
            };

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();

            var folderDto = new FolderDto
            {
                Id = folder.Id,
                Name = folder.Name,
                UserId = folder.UserId,
                DiaryEntryIds = Enumerable.Empty<int>()
            };

            return CreatedAtAction(nameof(GetFolder), new { id = folderDto.Id }, folderDto);
        }

        // PUT: api/Folders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFolder(int id, UpdateFolderDto updateDto)
        {
            var folder = await _context.Folders.FindAsync(id);

            if (folder == null)
            {
                return NotFound(new { Message = $"Folder with ID {id} not found." });
            }

            folder.Name = updateDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the folder." });
            }

            return NoContent();
        }

        // DELETE: api/Folders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            var folder = await _context.Folders.Include(f => f.DiaryEntries).FirstOrDefaultAsync(f => f.Id == id);

            if (folder == null)
            {
                return NotFound(new { Message = $"Folder with ID {id} not found." });
            }

            if (folder.DiaryEntries.Any())
            {
                return BadRequest(new { Message = "Folder contains diary entries and cannot be deleted." });
            }

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Folder with ID {id} deleted successfully." });
        }
    }
}
