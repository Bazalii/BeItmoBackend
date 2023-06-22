namespace BeItmoBackend.Core.UniversityEvents.Models;

public class UniversityEventCreationModel
{
    public int CreatorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Place { get; set; } = string.Empty;
    public string Contacts { get; set; } = string.Empty;
    public string PictureLink { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public List<Guid> InterestIds { get; set; } = new();
}