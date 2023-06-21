using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Interests.Models;

namespace BeItmoBackend.Core.Users.Models;

public class User
{
    public int Id { get; set; }
    public int FriendlinessScore { get; set; }
    public int HealthScore { get; set; }
    public int FitScore { get; set; }
    public int EcoScore { get; set; }
    public int OpenScore { get; set; }
    public int ProScore { get; set; }
    public List<Category> Categories { get; set; } = new();
    public List<Interest> Interests { get; set; } = new();
}