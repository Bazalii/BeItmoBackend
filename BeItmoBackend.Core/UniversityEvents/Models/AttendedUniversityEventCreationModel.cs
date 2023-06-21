namespace BeItmoBackend.Core.UniversityEvents.Models;

public class AttendedUniversityEventCreationModel
{
    public int UserId { get; set; }
    public Guid EventId { get; set; }
}