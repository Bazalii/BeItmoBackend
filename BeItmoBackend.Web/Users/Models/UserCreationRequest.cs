namespace BeItmoBackend.Web.Users.Models;

public class UserCreationRequest
{
    public List<Guid> CategoryIds { get; set; } = new();
    public List<Guid> InterestIds { get; set; } = new();
}