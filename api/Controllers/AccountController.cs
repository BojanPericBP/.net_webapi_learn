using System.Collections.ObjectModel;
using api.Dtos;
using api.Dtos.Account;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) : ControllerBase()
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerInput)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new AppUser
            {
                UserName = registerInput.Username,
                Email = registerInput.Email,
            };

            var createdUser = await userManager.CreateAsync(appUser, registerInput.Password!);


            if (createdUser.Succeeded)
            {
                var role = new Collection<string> { "User", };

                var roleResult = await userManager.AddToRolesAsync(appUser, role);

                if (roleResult.Succeeded)
                {
                    return Ok(
                        new NewUserDto
                        {
                            UserName = appUser.UserName!,
                            Email = appUser.Email!,
                            Token = tokenService.CreateToken(appUser)
                        }
                    );
                }

                else
                    return StatusCode(500, roleResult.Errors);
            }
            else
                return StatusCode(500, createdUser.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginInput)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginInput.Username.ToLower());

        if (user is null)
            return Unauthorized("Invalid username");

        var result = await signInManager.CheckPasswordSignInAsync(user, loginInput.Password, false);

        if (!result.Succeeded)
            return Unauthorized("Username not found and/or password incorrect");

        return Ok(new NewUserDto
        {
            UserName = user.UserName,
            Token = tokenService.CreateToken(user),
            Email = user.Email
        });
    }
}
