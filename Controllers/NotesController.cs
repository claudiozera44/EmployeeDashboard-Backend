using EmployeeDashboard.Api.Models;
using EmployeeDashboard.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDashboard.Api.Controllers;

[ApiController]
[Route("api/employees/{employeeId}/notes")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly ILogger<NotesController> _logger;

    public NotesController(
        INoteService noteService,
        ILogger<NotesController> logger)
    {
        _noteService = noteService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Note>>> GetNotes(string employeeId)
    {
        try
        {
            var notes = await _noteService.GetNotesForEmployeeAsync(employeeId);
            return Ok(notes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving notes for employee {EmployeeId}", employeeId);
            return StatusCode(500, new { error = "An error occurred while retrieving notes" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Note>> CreateNote(string employeeId, [FromBody] CreateNoteRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return BadRequest(new { error = "Note content is required" });
            }

            var note = await _noteService.AddNoteAsync(employeeId, request.Content);
            return CreatedAtAction(nameof(GetNotes), new { employeeId }, note);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid request for creating note");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating note for employee {EmployeeId}", employeeId);
            return StatusCode(500, new { error = "An error occurred while creating the note" });
        }
    }
}
