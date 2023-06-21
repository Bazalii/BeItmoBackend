using BeItmoBackend.Core.Users.Models;
using BeItmoBackend.Web.Users.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Web.Users.Mappers;

[Mapper]
public partial class UserWebModelsMapper
{
    public partial UserResponse MapUserToUserResponse(User user);
}