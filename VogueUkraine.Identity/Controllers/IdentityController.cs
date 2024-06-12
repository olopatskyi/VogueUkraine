using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VogueUkraine.Framework.HttpController;
using VogueUkraine.Identity.Managers.Abstractions;
using VogueUkraine.Identity.Models.Requests;

namespace VogueUkraine.Identity.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : HttpController
{
    private readonly IIdentityManager _identityManager;

    public IdentityController(IIdentityManager identityManager)
    {
        _identityManager = identityManager;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUpAsync(SignUpModelRequest request, CancellationToken cancellationToken)
    {
        var result = await _identityManager.SignUpAsync(request, cancellationToken);
        return ActionResult(result);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync(SignInModelRequest request, CancellationToken cancellationToken)
    {
        var result = await _identityManager.SignInAsync(request, cancellationToken);
        return ActionResult(result);
    }

    [HttpPost("refresh")]
    [Authorize]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenModelRequest request,
        CancellationToken cancellationToken)
    {
        request.UserId = GetUserId();
        var result = await _identityManager.RefreshTokenAsync(request, cancellationToken);
        return ActionResult(result);
    }
}