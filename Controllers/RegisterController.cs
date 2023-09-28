using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTOs;
using NZwalks.API.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtInterface jwtInterface;

        public RegisterController(UserManager<IdentityUser> userManager,JwtInterface jwtInterface)
        {
            this.userManager = userManager;
            this.jwtInterface = jwtInterface;
        }



        //register user 
        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> register([FromBody] registerDto registerDto )
        {
            var identityuser = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Username
            };

            var result = await userManager.CreateAsync(identityuser,registerDto.Password);

            if(result.Succeeded)
            {
                result = await userManager.AddToRolesAsync(identityuser, registerDto.Roles);
                if (result.Succeeded)
                {
                    return Ok("user registered");
                }
            }
            return BadRequest("something went wrong");
        }

        // login user
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] loginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Username);
            if (user != null) {
            var checkpasswordresult = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (checkpasswordresult)
                {
                    //create token
                    var roles = await  userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwttoken = jwtInterface.CreateJwtToken(user, roles.ToList());
                        var response = new LoginresponseDto
                        {
                            JwtToken = jwttoken,
                        };
                        return Ok(response);
                    }

                    return Ok();    
                }
            }
            return BadRequest("username and password incoorect");
        }


    }
}
