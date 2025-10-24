namespace EmployeeDashboard.Api.Models;

public class Note
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EmployeeId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class CreateNoteRequest
{
    public string Content { get; set; } = string.Empty;
}
