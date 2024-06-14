using Microsoft.AspNetCore.Mvc;
using VogueUkraine.Framework.HttpController;
using VogueUkraine.Management.Api.Managers.Abstractions;
using VogueUkraine.Management.Api.Models.Requests;

namespace VogueUkraine.Management.Api.Controllers;

[Route("api/profile/contestants")]
[ApiController]
public class ParticipantController : HttpController
{
    private readonly IParticipantManager _participantManager;

    public ParticipantController(IParticipantManager participantManager)
    {
        _participantManager = participantManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreateParticipantModelRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _participantManager.CreateAsync(request, cancellationToken);
        return ActionResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] DeleteParticipantModelRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _participantManager.DeleteAsync(request, cancellationToken);
        return ActionResult(result);
    }
}