using Azure.Identity;
using BackEnd_S7_L1.Models.Dto;
using BackEnd_S7_L1.Models.Dto.Request;
using BackEnd_S7_L1.Models.Dto.Response;
using BackEnd_S7_L1.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BackEnd_S7_L1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //costruttore
        public AspNetUserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        UserName = registerRequestDto.UserName,
                        Email = registerRequestDto.Email,
                        Nome = registerRequestDto.Nome,
                        Cognome = registerRequestDto.Cognome,
                        PhoneNumber = registerRequestDto.PhoneNumber,
                        Birthday = registerRequestDto.Birthday,
                        CreateAt = DateTime.UtcNow,
                        Id = Guid.NewGuid().ToString(),
                        IsDeleted = false,
                        EmailConfirmed = true,
                        LockoutEnabled = false,


                    };

                    IdentityResult result = await _userManager.CreateAsync(user, registerRequestDto.Password);
                    if (result.Succeeded)
                    {
                        var roleExist = await this._roleManager.RoleExistsAsync("User");
                        if (!roleExist)
                        {
                            await this._roleManager.CreateAsync(new IdentityRole("User"));
                        }
                        await this._userManager.AddToRoleAsync(user, "User");
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginRequestDto.UserName);
                if (user is null)
                {
                    return BadRequest();
                }

                Microsoft.AspNetCore.Identity.SignInResult result = await this._signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, false);
                if (!result.Succeeded)
                {
                    return BadRequest();

                }
                List<string> roles = (await this._userManager.GetRolesAsync(user)).ToList();

                List<Claim> userClaims = new List<Claim>();
                foreach (string roleName in roles)
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, roleName));

                }


                var key = System.Text.Encoding.UTF8.GetBytes("9024b53e1052044d789a01b830667b0f49876314b0483ebe56622285ff3eb49e");

                SigningCredentials cred = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256);


                var tokenExpiration = DateTime.Now.AddMinutes(30);

                JwtSecurityToken jwt = new JwtSecurityToken(
                    "https://",
                    "https://",
                     claims: userClaims,
                      expires: tokenExpiration,
                    signingCredentials: cred

                    );

                string token = new JwtSecurityTokenHandler().WriteToken(jwt);
                return Ok(new LoginResponseDto()
                {
                    Token = token,
                    Expiration = tokenExpiration

                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }
    }
}
