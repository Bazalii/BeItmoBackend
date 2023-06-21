namespace BeItmoBackend.Web.UniversityEvents.Models;

public class AttendedUniversityEventUpdateRequest
{
    public Guid EventId { get; set; }
    public int Score { get; set; }
}