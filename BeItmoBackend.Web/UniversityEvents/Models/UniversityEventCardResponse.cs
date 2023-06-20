namespace BeItmoBackend.Web.UniversityEvents.Models;

public class UniversityEventCardResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public string PictureLink { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}