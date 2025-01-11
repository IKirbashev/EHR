using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicHealthRecord.Data;
using ElectronicHealthRecord.Models;
using ElectronicHealthRecord.DTOs;

namespace ElectronicHealthRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiomarkersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BiomarkersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Biomarkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BiomarkerDto>>> GetBiomarkers()
        {
            var biomarkers = await _context.Biomarkers
                .Select(b => new BiomarkerDto
                {
                    Id = b.Id,
                    Type = b.Type,
                    Value = b.Value,
                    Date = b.Date,
                    UserId = b.UserId
                })
                .ToListAsync();

            return Ok(biomarkers);
        }

        // GET: api/Biomarkers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BiomarkerDto>> GetBiomarker(int id)
        {
            var biomarker = await _context.Biomarkers
                .Where(b => b.Id == id)
                .Select(b => new BiomarkerDto
                {
                    Id = b.Id,
                    Type = b.Type,
                    Value = b.Value,
                    Date = b.Date,
                    UserId = b.UserId
                })
                .FirstOrDefaultAsync();

            if (biomarker == null)
            {
                return NotFound(new { Message = $"Biomarker with ID {id} not found." });
            }

            return Ok(biomarker);
        }

        // POST: api/Biomarkers
        [HttpPost]
        public async Task<ActionResult<BiomarkerDto>> PostBiomarker(CreateBiomarkerDto createDto)
        {
            var biomarker = new Biomarker
            {
                Type = createDto.Type,
                Value = createDto.Value,
                Date = createDto.Date,
                UserId = createDto.UserId
            };

            _context.Biomarkers.Add(biomarker);
            await _context.SaveChangesAsync();

            var biomarkerDto = new BiomarkerDto
            {
                Id = biomarker.Id,
                Type = biomarker.Type,
                Value = biomarker.Value,
                Date = biomarker.Date,
                UserId = biomarker.UserId
            };

            return CreatedAtAction(nameof(GetBiomarker), new { id = biomarkerDto.Id }, biomarkerDto);
        }

        // PUT: api/Biomarkers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBiomarker(int id, UpdateBiomarkerDto updateDto)
        {
            var biomarker = await _context.Biomarkers.FindAsync(id);

            if (biomarker == null)
            {
                return NotFound(new { Message = $"Biomarker with ID {id} not found." });
            }

            biomarker.Type = updateDto.Type;
            biomarker.Value = updateDto.Value;
            biomarker.Date = updateDto.Date;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the biomarker." });
            }

            return NoContent();
        }

        // DELETE: api/Biomarkers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBiomarker(int id)
        {
            var biomarker = await _context.Biomarkers.FindAsync(id);

            if (biomarker == null)
            {
                return NotFound(new { Message = $"Biomarker with ID {id} not found." });
            }

            _context.Biomarkers.Remove(biomarker);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Biomarker with ID {id} deleted successfully." });
        }
    }
}
