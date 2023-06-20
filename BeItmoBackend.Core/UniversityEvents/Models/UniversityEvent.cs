using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Interests.Models;

namespace BeItmoBackend.Core.UniversityEvents.Models;

public class UniversityEvent
{
    public Guid Id { get; set; }
    public int CreatorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Place { get; set; } = string.Empty;
    public string Contacts { get; set; } = string.Empty;
    public string PictureLink { get; set; } = string.Empty;
    public Category Category { get; set; } = new();
    public List<Interest> Interests { get; set; } = new();
}