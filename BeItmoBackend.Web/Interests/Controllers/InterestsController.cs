using System.Net.Mime;
using BeItmoBackend.Core.Interests.Services;
using BeItmoBackend.Web.Interests.Mappers;
using BeItmoBackend.Web.Interests.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeItmoBackend.Web.Interests.Controllers;

[ApiController]
[Route("interests")]
[Produces(MediaTypeNames.Application.Json)]
public class InterestsController
{
    private readonly IInterestService _interestService;
    private readonly InterestWebModelsMapper _mapper;

    public InterestsController(IInterestService interestService, InterestWebModelsMapper mapper)
    {
        _interestService = interestService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<InterestResponse> AddAsync(InterestCreationRequest creationRequest,
                                                 CancellationToken cancellationToken)
    {
        var interest = await _interestService
            .AddAsync(_mapper.MapCreationRequestToCreationModel(creationRequest), cancellationToken);

        return _mapper.MapInterestToResponse(interest);
    }

    [HttpGet]
    public async Task<IEnumerable<InterestResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var interests = await _interestService.GetAllAsync(cancellationToken);

        return interests.Select(category => _mapper.MapInterestToResponse(category));
    }

    [HttpDelete("{id:guid}")]
    public Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _interestService.RemoveByIdAsync(id, cancellationToken);
    }
}