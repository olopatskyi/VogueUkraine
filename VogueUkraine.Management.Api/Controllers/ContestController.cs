using Microsoft.AspNetCore.Mvc;
using VogueUkraine.Framework.HttpController;
using VogueUkraine.Management.Api.Managers.Abstractions;
using VogueUkraine.Management.Api.Models.Requests;

namespace VogueUkraine.Management.Api.Controllers;

[ApiController]
[Route("api/contests")]
public class ContestController : HttpController
{
    private readonly IContestManager _contestManager;
    private readonly IVoteManager _voteManager;

    public ContestController(IContestManager contestManager, IVoteManager voteManager)
    {
        _contestManager = contestManager;
        _voteManager = voteManager;
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

    [HttpPut("{id}/participants")]
    public async Task<IActionResult> AddParticipantsAsync(string id, AddParticipantsModelRequest request,
        CancellationToken cancellationToken = default)
    {
        request.ContestId = id;
        var response = await _contestManager.AddParticipantsAsync(request, cancellationToken);
        return ActionResult(response);
    }

    [HttpPost("{contestId}/vote/{participantId}")]
    public async Task<IActionResult> CreateVoteAsync(string contestId, string participantId,
        CancellationToken cancellationToken)
    {
        var result = await _voteManager.CreateVoteAsync(new CreateVoteModelRequest
        {
            ContestId = contestId,
            ParticipantId = participantId
        }, cancellationToken);
        return ActionResult(result);
    }
}