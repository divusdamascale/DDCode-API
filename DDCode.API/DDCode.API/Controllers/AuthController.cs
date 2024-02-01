using DDCode.API.Models.DTO;
using DDCode.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DDCode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository) : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDTO request)
        {
            var user = new IdentityUser
            {
                UserName = request.Email.TrimEnd(),
                Email = request.Email.TrimEnd(),
            };
            var identityResult = await userManager.CreateAsync(user,request.Password);

            if(identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user,"Reader");
                if(identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if(identityResult.Errors.Any())
                    {
                        foreach(var error in identityResult.Errors)
                        {
                            ModelState.AddModelError(error.Code,error.Description);
                        }
                    }
                }
            }
            else
            {
                if(identityResult.Errors.Any())
                {
                    foreach(var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(error.Code,error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var user = await userManager.FindByEmailAsync(request.Email.TrimEnd());
            if (user is not null)
            {
                var result = await userManager.CheckPasswordAsync(user, request.Password);
                if (result)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var token = tokenRepository.CreateToken(user, roles.ToList());
                    var response = new LoginResponseDTO
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = token
                    };
                    return Ok(response);
                }
            }
            ModelState.AddModelError("","Invalid Credentials");
            return ValidationProblem(ModelState);
        }
    }
}
