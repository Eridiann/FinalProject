using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RootRPGAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RootRPGAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{

		private readonly UserManager<IdentityUser> userManager;
		private readonly IConfiguration configuration;
		private readonly SignInManager<IdentityUser> signInManager;

		public UserController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
		{
			this.userManager = userManager;
			this.configuration = configuration;
			this.signInManager = signInManager;
		}

		[HttpPost("register")]
		public async Task<ActionResult<AuthResponse>> RegisterUser(myUser user)
		{
			var IdUser = new IdentityUser
			{
				UserName = user.UserName,
				Email = user.Email,
			};
			var result = await userManager.CreateAsync(IdUser, user.Password);

			if (result.Succeeded)
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost("login")]
		public async Task<ActionResult<AuthResponse>> LoginUser(myUser user)
		{
			var result = await signInManager.PasswordSignInAsync(user.UserName, user.Password, isPersistent: true, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				return GenerateToken(user);
			}
			else
			{
				return BadRequest();
			}
		}

		private AuthResponse GenerateToken(myUser user)
		{
			var claims = new List<Claim>
			{
				new Claim("usuario", user.UserName),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtkey"]));
			var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiration = DateTime.Now.AddDays(1);
			var securityToken = new JwtSecurityToken(issuer: null, claims: claims, expires: expiration, signingCredentials: credential);

			return new AuthResponse()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
				Expiration = expiration,
			};
		}
	}
}