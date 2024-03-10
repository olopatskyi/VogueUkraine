using Microsoft.AspNetCore.Mvc;
using VogueUkraine.Framework.HttpController;
using VogueUkraine.Profile.Api.Managers.Abstractions;
using VogueUkraine.Profile.Api.Models.Requests;

namespace VogueUkraine.Profile.Api.Controllers;

[Route("api/profile/contestants")]
[ApiController]
public class ContestantController : HttpController
{
    private readonly IContestantManager _contestantManager;

    public ContestantController(IContestantManager contestantManager)
    {
        _contestantManager = contestantManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreateContestantModelRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _contestantManager.CreateAsync(request, cancellationToken);
        return ActionResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] DeleteContestantModelRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _contestantManager.DeleteAsync(request, cancellationToken);
        return ActionResult(result);
    }
}