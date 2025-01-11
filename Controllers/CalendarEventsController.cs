using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicHealthRecord.Data;
using ElectronicHealthRecord.Models;
using ElectronicHealthRecord.DTOs;

namespace ElectronicHealthRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalendarEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CalendarEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEventDto>>> GetCalendarEvents()
        {
            var events = await _context.CalendarEvents
                .Select(e => new CalendarEventDto
                {
                    Id = e.Id,
                    Date = e.Date,
                    Description = e.Description,
                    EventType = e.EventType,
                    UserId = e.UserId
                })
                .ToListAsync();

            return Ok(events);
        }

        // GET: api/CalendarEvents/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarEventDto>> GetCalendarEvent(int id)
        {
            var calendarEvent = await _context.CalendarEvents
                .Where(e => e.Id == id)
                .Select(e => new CalendarEventDto
                {
                    Id = e.Id,
                    Date = e.Date,
                    Description = e.Description,
                    EventType = e.EventType,
                    UserId = e.UserId
                })
                .FirstOrDefaultAsync();

            if (calendarEvent == null)
            {
                return NotFound(new { Message = $"Calendar event with ID {id} not found." });
            }

            return Ok(calendarEvent);
        }

        // POST: api/CalendarEvents
        [HttpPost]
        public async Task<ActionResult<CalendarEventDto>> PostCalendarEvent(CreateCalendarEventDto createDto)
        {
            var calendarEvent = new CalendarEvent
            {
                Date = createDto.Date,
                Description = createDto.Description,
                EventType = createDto.EventType,
                UserId = createDto.UserId
            };

            _context.CalendarEvents.Add(calendarEvent);
            await _context.SaveChangesAsync();

            var calendarEventDto = new CalendarEventDto
            {
                Id = calendarEvent.Id,
                Date = calendarEvent.Date,
                Description = calendarEvent.Description,
                EventType = calendarEvent.EventType,
                UserId = calendarEvent.UserId
            };

            return CreatedAtAction(nameof(GetCalendarEvent), new { id = calendarEventDto.Id }, calendarEventDto);
        }

        // PUT: api/CalendarEvents/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendarEvent(int id, UpdateCalendarEventDto updateDto)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);

            if (calendarEvent == null)
            {
                return NotFound(new { Message = $"Calendar event with ID {id} not found." });
            }

            calendarEvent.Date = updateDto.Date;
            calendarEvent.Description = updateDto.Description;
            calendarEvent.EventType = updateDto.EventType;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the calendar event." });
            }

            return NoContent();
        }

        // DELETE: api/CalendarEvents/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendarEvent(int id)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound(new { Message = $"Calendar event with ID {id} not found." });
            }

            _context.CalendarEvents.Remove(calendarEvent);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Calendar event with ID {id} deleted successfully." });
        }
    }
}
