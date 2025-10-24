using EmployeeDashboard.Api.Models;
using System.Collections.Concurrent;

namespace EmployeeDashboard.Api.Services;

public class NoteService : INoteService
{
    private readonly ConcurrentDictionary<string, List<Note>> _notesStore = new();
    private readonly ILogger<NoteService> _logger;

    public NoteService(ILogger<NoteService> logger)
    {
        _logger = logger;
    }

    public Task<List<Note>> GetNotesForEmployeeAsync(string employeeId)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
        {
            _logger.LogWarning("GetNotesForEmployeeAsync called with empty employeeId");
            return Task.FromResult(new List<Note>());
        }

        var notes = _notesStore.GetOrAdd(employeeId, _ => new List<Note>());
        return Task.FromResult(notes.OrderByDescending(n => n.CreatedAt).ToList());
    }

    public Task<Note> AddNoteAsync(string employeeId, string content)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
        {
            throw new ArgumentException("Employee ID cannot be empty", nameof(employeeId));
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Note content cannot be empty", nameof(content));
        }

        var note = new Note
        {
            Id = Guid.NewGuid().ToString(),
            EmployeeId = employeeId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        var notes = _notesStore.GetOrAdd(employeeId, _ => new List<Note>());
        notes.Add(note);

        _logger.LogInformation("Note {NoteId} added for employee {EmployeeId}", note.Id, employeeId);

        return Task.FromResult(note);
    }
}
