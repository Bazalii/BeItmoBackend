namespace BeItmoBackend.Web.UniversityEvents.Models;

public class UniversityEventResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Place { get; set; } = string.Empty;
    public string Contacts { get; set; } = string.Empty;
    public string PictureLink { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public List<string> InterestNames { get; set; } = new();
}