using EmployeeDashboard.Api.Models;

namespace EmployeeDashboard.Api.Services;

public interface INoteService
{
    Task<List<Note>> GetNotesForEmployeeAsync(string employeeId);
    Task<Note> AddNoteAsync(string employeeId, string content);
}
