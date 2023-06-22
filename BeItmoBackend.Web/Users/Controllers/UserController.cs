using System.Net.Mime;
using BeItmoBackend.Core.Users.Models;
using BeItmoBackend.Core.Users.Services;
using BeItmoBackend.Web.Users.Mappers;
using BeItmoBackend.Web.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeItmoBackend.Web.Users.Controllers;

[ApiController]
[Route("users")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly UserWebModelsMapper _mapper;

    public UserController(IUserService userService, UserWebModelsMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<UserResponse> AddAsync(UserCreationRequest creationRequest, CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var creationModel = new UserCreationModel
        {
            Id = userId,
            CategoryIds = creationRequest.CategoryIds,
            InterestIds = creationRequest.InterestIds
        };

        var addedUser = await _userService.AddAsync(creationModel, cancellationToken);

        return _mapper.MapUserToUserResponse(addedUser);
    }

    [HttpGet]
    public async Task<UserResponse> GetByIdAsync(CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var user = await _userService.GetByIdAsync(userId, cancellationToken);

        return _mapper.MapUserToUserResponse(user);
    }

    [HttpGet("exists")]
    public Task<bool> ExistsAsync(CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        return _userService.ExistsAsync(userId, cancellationToken);
    }

    [HttpPut("categories")]
    public async Task UpdateCategoriesAsync(UpdateUserCategoriesRequest updateRequest,
                                            CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        await _userService.UpdateCategoriesAsync(userId, updateRequest.CategoryIds, cancellationToken);
    }

    [HttpPut("interests")]
    public async Task UpdateInterestsAsync(UpdateUserInterestsRequest updateRequest,
                                           CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        await _userService.UpdateInterestsAsync(userId, updateRequest.InterestIds, cancellationToken);
    }

    [HttpDelete("{id:int}")]
    public Task RemoveByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _userService.RemoveByIdAsync(id, cancellationToken);
    }
}