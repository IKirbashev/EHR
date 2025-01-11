using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicHealthRecord.Data;
using ElectronicHealthRecord.Models;
using ElectronicHealthRecord.DTOs;

namespace ElectronicHealthRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Medications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationDto>>> GetMedications()
        {
            var medications = await _context.Medications
                .Select(m => new MedicationDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Dosage = m.Dosage,
                    Schedule = m.Schedule,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    UserId = m.UserId
                })
                .ToListAsync();

            return Ok(medications);
        }

        // GET: api/Medications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationDto>> GetMedication(int id)
        {
            var medication = await _context.Medications
                .Where(m => m.Id == id)
                .Select(m => new MedicationDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Dosage = m.Dosage,
                    Schedule = m.Schedule,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    UserId = m.UserId
                })
                .FirstOrDefaultAsync();

            if (medication == null)
            {
                return NotFound(new { Message = $"Medication with ID {id} not found." });
            }

            return Ok(medication);
        }

        // POST: api/Medications
        [HttpPost]
        public async Task<ActionResult<MedicationDto>> PostMedication(CreateMedicationDto createDto)
        {
            var medication = new Medication
            {
                Name = createDto.Name,
                Dosage = createDto.Dosage,
                Schedule = createDto.Schedule,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                UserId = createDto.UserId
            };

            _context.Medications.Add(medication);
            await _context.SaveChangesAsync();

            var medicationDto = new MedicationDto
            {
                Id = medication.Id,
                Name = medication.Name,
                Dosage = medication.Dosage,
                Schedule = medication.Schedule,
                StartDate = medication.StartDate,
                EndDate = medication.EndDate,
                UserId = medication.UserId
            };

            return CreatedAtAction(nameof(GetMedication), new { id = medicationDto.Id }, medicationDto);
        }

        // PUT: api/Medications/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedication(int id, UpdateMedicationDto updateDto)
        {
            var medication = await _context.Medications.FindAsync(id);

            if (medication == null)
            {
                return NotFound(new { Message = $"Medication with ID {id} not found." });
            }

            medication.Name = updateDto.Name;
            medication.Dosage = updateDto.Dosage;
            medication.Schedule = updateDto.Schedule;
            medication.StartDate = updateDto.StartDate;
            medication.EndDate = updateDto.EndDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the medication." });
            }

            return NoContent();
        }

        // DELETE: api/Medications/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            var medication = await _context.Medications.FindAsync(id);

            if (medication == null)
            {
                return NotFound(new { Message = $"Medication with ID {id} not found." });
            }

            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Medication with ID {id} deleted successfully." });
        }
    }
}
