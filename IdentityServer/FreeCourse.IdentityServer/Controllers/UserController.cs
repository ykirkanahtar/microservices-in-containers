using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FreeCourse.IdentityServer.Controllers;

[Authorize(IdentityServerConstants.LocalApi.PolicyName)]
[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignupDto input)
    {
        var user = new ApplicationUser
        {
            UserName = input.UserName,
            Email = input.Email,
            City = input.City
        };

        var result = await _userManager.CreateAsync(user, input.Password);

        if (result.Succeeded == false)
        {
            return BadRequest(
                Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(),
                    400));
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

        if (userIdClaim == null)
        {
            return BadRequest();
        }

        var user = await _userManager.FindByIdAsync(userIdClaim.Value);

        if (user == null)
        {
            return BadRequest();
        }

        return Ok(new
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            City = user.City
        });
    }
}