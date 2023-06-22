namespace BeItmoBackend.Web.UniversityEvents.Models;

public class AttendedUniversityEventResponse
{
    public int UserId { get; set; }
    public Guid EventId { get; set; }
    public int Score { get; set; }
}