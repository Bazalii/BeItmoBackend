namespace BeItmoBackend.Web.Users.Models;

public class UpdateUserCategoriesRequest
{
    public List<Guid> CategoryIds { get; set; } = new();
}