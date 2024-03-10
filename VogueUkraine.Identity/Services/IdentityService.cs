using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using VogueUkraine.Framework;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Identity.Extensions;
using VogueUkraine.Identity.Models.Requests;
using VogueUkraine.Identity.Services.Abstractions;

namespace VogueUkraine.Identity.Services;

public class IdentityService : LogicalLayerElement, IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ServiceResponse<ValidationResult>> CreateAsync(CreateUserModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return ValidationFailure(new ValidationResult
            {
                Errors =
                {
                    new ValidationFailure(nameof(CreateUserModelRequest.Email), ValidationMessages.UserAlreadyExists)
                }
            });
        }

        user = new IdentityUser
        {
            EmailConfirmed = true,
            UserName = $"{request.FirstName} {request.LastName}",
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            return result.ToServiceResponse();
        }

        return Success();
    }
}