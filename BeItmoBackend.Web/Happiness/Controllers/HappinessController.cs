using System.Net.Mime;
using BeItmoBackend.Core.Happiness.Models;
using BeItmoBackend.Core.Happiness.Services;
using BeItmoBackend.Web.Happiness.Mappers;
using BeItmoBackend.Web.Happiness.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeItmoBackend.Web.Happiness.Controllers;

[ApiController]
[Route("happiness")]
[Produces(MediaTypeNames.Application.Json)]
public class HappinessController : Controller
{
    private readonly IHappinessService _happinessService;
    private readonly HappinessWebModelsMapper _mapper;

    public HappinessController(
        IHappinessService happinessService,
        HappinessWebModelsMapper mapper)
    {
        _happinessService = happinessService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<HappinessCheckpointResponse> AddAsync(HappinessCheckpointCreationRequest creationRequest,
                                                            CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var addedHappinessCheckpoint =
            await _happinessService.AddAsync(
                new HappinessCheckpointCreationModel
                {
                    UserId = userId,
                    Note = creationRequest.Note,
                    Score = creationRequest.Score
                },
                cancellationToken);

        return _mapper.MapHappinessCheckpointToResponse(addedHappinessCheckpoint);
    }

    [HttpGet("all/{startDate:datetime}/{endDate:datetime}")]
    public async Task<IEnumerable<HappinessCheckpointResponse>> GetAllForUserInTimeRangeAsync(
        DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var checkpoints =
            await _happinessService.GetAllForUserInTimeRangeAsync(userId, startDate, endDate, cancellationToken);

        return checkpoints.Select(checkpoint => _mapper.MapHappinessCheckpointToResponse(checkpoint));
    }

    [HttpGet("exists/{date:datetime}")]
    public Task<bool> ExistsAsync(DateOnly date, CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        return _happinessService.ExistsAsync(userId, date, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _happinessService.RemoveByIdAsync(id, cancellationToken);
    }

    [HttpPost("calculateHappiness")]
    public Task<int> CalculateHappinessScoreAsync(CalculateHappinessRequest request,
                                                  CancellationToken cancellationToken)
    {
        return _happinessService.CalculateHappinessScoreAsync(request.Message, cancellationToken);
    }
}