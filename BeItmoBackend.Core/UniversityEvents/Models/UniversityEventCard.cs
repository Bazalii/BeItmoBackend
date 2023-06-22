using BeItmoBackend.Core.Categories.Models;

namespace BeItmoBackend.Core.UniversityEvents.Models;

public class UniversityEventCard
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public string PictureLink { get; set; } = string.Empty;
    public Category Category { get; set; } = new();
}