using BeItmoBackend.Web.Categories.Models;
using BeItmoBackend.Web.Interests.Models;

namespace BeItmoBackend.Web.Users.Models;

public class UserResponse
{
    public int Id { get; set; }
    public int FriendlinessScore { get; set; }
    public int HealthScore { get; set; }
    public int FitScore { get; set; }
    public int EcoScore { get; set; }
    public int OpenScore { get; set; }
    public int ProScore { get; set; }
    public List<CategoryResponse> Categories { get; set; } = new();
    public List<InterestResponse> Interests { get; set; } = new();
}