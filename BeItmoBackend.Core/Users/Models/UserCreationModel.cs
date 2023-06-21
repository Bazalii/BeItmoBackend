namespace BeItmoBackend.Core.Users.Models;

public class UserCreationModel
{
    public int Id { get; set; }
    public List<Guid> CategoryIds { get; set; } = new();
    public List<Guid> InterestIds { get; set; } = new();
}