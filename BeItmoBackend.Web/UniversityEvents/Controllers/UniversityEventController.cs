using System.Net.Mime;
using BeItmoBackend.Core.UniversityEvents.Models;
using BeItmoBackend.Core.UniversityEvents.Services;
using BeItmoBackend.Web.UniversityEvents.Mappers;
using BeItmoBackend.Web.UniversityEvents.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeItmoBackend.Web.UniversityEvents.Controllers;

[ApiController]
[Route("events")]
[Produces(MediaTypeNames.Application.Json)]
public class UniversityEventController : Controller
{
    private readonly IUniversityEventService _universityEventService;
    private readonly UniversityEventWebModelsMapper _mapper;

    public UniversityEventController(
        IUniversityEventService universityEventService,
        UniversityEventWebModelsMapper mapper)
    {
        _universityEventService = universityEventService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<UniversityEventResponse> AddAsync(UniversityEventCreationRequest creationRequest,
                                                        CancellationToken cancellationToken)
    {
        var creationModel = _mapper.MapCreationRequestToCreationModel(creationRequest);

        creationModel.CreatorId = (int) HttpContext.Items["isuNumber"]!;

        var universityEvent = await _universityEventService.AddAsync(creationModel, cancellationToken);

        return _mapper.MapUniversityEventToResponse(universityEvent);
    }

    [HttpGet("{id:guid}")]
    public async Task<UniversityEventResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var universityEvent = await _universityEventService
            .GetByIdAsync(id, cancellationToken);

        return _mapper.MapUniversityEventToResponse(universityEvent);
    }

    [HttpGet("newForUser")]
    public async Task<UniversityEventCardResponse> GetNewForUserAsync(CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var card = await _universityEventService.GetNewForUserAsync(userId, cancellationToken);

        return _mapper.MapUniversityEventCardToResponse(card);
    }

    [HttpGet("personalised/{number:int}")]
    public async Task<IEnumerable<UniversityEventCardResponse>> GetPersonalisedAsync(
        int number, CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var cards = await _universityEventService.GetPersonalisedAsync(number, userId, cancellationToken);

        return cards.Select(card => _mapper.MapUniversityEventCardToResponse(card));
    }

    [HttpGet("all/{pageNumber:int}/{pageSize:int}")]
    public async Task<IEnumerable<UniversityEventCardResponse>> GetAllAsync(int pageNumber, int pageSize,
                                                                            CancellationToken cancellationToken)
    {
        var cards = await _universityEventService.GetAllAsync(pageNumber, pageSize, cancellationToken);

        return cards.Select(card => _mapper.MapUniversityEventCardToResponse(card));
    }

    [HttpDelete]
    public Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _universityEventService.RemoveByIdAsync(id, cancellationToken);
    }

    [HttpPost("attend")]
    public async Task<AttendedUniversityEventResponse> AttendEventAsync(AttendedUniversityEventCreationRequest request,
                                                                        CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var addedAttendedEvent =
            await _universityEventService.AttendEventAsync(
                new AttendedUniversityEventCreationModel
                {
                    UserId = userId,
                    EventId = request.EventId
                }, cancellationToken);

        return _mapper.MapAttendedEventToResponse(addedAttendedEvent);
    }

    [HttpGet("allNotRated")]
    public async Task<IEnumerable<UniversityEventResponse>> GetAllNotRatedAttendedEventsForUserAsync(
        CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var events = await _universityEventService.GetAllNotRatedAttendedEventsForUserAsync(userId, cancellationToken);

        return events.Select(universityEvent => _mapper.MapUniversityEventToResponse(universityEvent));
    }

    [HttpGet("allRated")]
    public async Task<IEnumerable<UniversityEventResponse>> GetAllRatedAttendedEventsForUserAsync(
        int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var events =
            await _universityEventService.GetAllRatedAttendedEventsForUserAsync(
                userId, pageNumber, pageSize, cancellationToken);

        return events.Select(universityEvent => _mapper.MapUniversityEventToResponse(universityEvent));
    }

    [HttpPut("rate")]
    public async Task<AttendedUniversityEventResponse> RateAttendedEventAsync(
        AttendedUniversityEventUpdateRequest request,
        CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        var updatedEvent =
            await _universityEventService.RateAttendedEventAsync(
                new AttendedUniversityEvent
                {
                    UserId = userId,
                    EventId = request.EventId,
                    Score = request.Score
                },
                cancellationToken);

        return _mapper.MapAttendedEventToResponse(updatedEvent);
    }

    [HttpDelete("attended")]
    public Task RemoveEventFromAttendedAsync(Guid eventId, CancellationToken cancellationToken)
    {
        var userId = (int) HttpContext.Items["isuNumber"]!;

        return _universityEventService.RemoveEventFromAttendedAsync(userId, eventId, cancellationToken);
    }
}