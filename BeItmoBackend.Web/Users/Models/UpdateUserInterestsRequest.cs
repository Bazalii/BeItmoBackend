namespace BeItmoBackend.Web.Users.Models;

public class UpdateUserInterestsRequest
{
    public List<Guid> InterestIds { get; set; } = new();
}