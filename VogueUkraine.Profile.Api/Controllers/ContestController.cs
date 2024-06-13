using Microsoft.AspNetCore.Mvc;
using VogueUkraine.Framework.HttpController;
using VogueUkraine.Profile.Api.Managers.Abstractions;
using VogueUkraine.Profile.Api.Models.Requests;

namespace VogueUkraine.Profile.Api.Controllers;

[ApiController]
[Route("api/contests")]
public class ContestController : HttpController
{
    private readonly IContestManager _contestManager;

    public ContestController(IContestManager contestManager)
    {
        _contestManager = contestManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateContestModelRequest request, CancellationToken cancellationToken)
    {
        var response = await _contestManager.CreateAsync(request, cancellationToken);
        return ActionResult(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await _contestManager.GetByIdAsync(new GetOneContestModelRequest
        {
            Id = id
        }, cancellationToken);
        return ActionResult(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await _contestManager.UpdateAsync(new UpdateContestModelRequest
        {
            Id = id
        }, cancellationToken);
        return ActionResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> FinishContestAsync(SetWinnerModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await _contestManager.SetWinnerAsync(request, cancellationToken);
        return ActionResult(response);
    }
}