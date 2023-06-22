namespace BeItmoBackend.Core.UniversityEvents.Models;

public class AttendedUniversityEvent
{
    public int UserId { get; set; }
    public Guid EventId { get; set; }
    public int Score { get; set; }
}