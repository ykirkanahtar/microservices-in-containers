using Duende.IdentityServer.Validation;
using FreeCourse.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace FreeCourse.IdentityServer.Services;

public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var existUser = await _userManager.FindByEmailAsync(context.UserName);

        if (existUser == null)
        {
            var errors = new Dictionary<string, object> { { "errors", new List<string>{"E-posta ya da parolanız hatalı"} } };

            context.Result.CustomResponse = errors;
            return;
        }

        var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);

        if (passwordCheck == false)
        {
            var errors = new Dictionary<string, object> { { "errors", new List<string>{"E-posta ya da parolanız hatalı"} } };

            context.Result.CustomResponse = errors;
            return;
        }

        context.Result =
            new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
    }
}