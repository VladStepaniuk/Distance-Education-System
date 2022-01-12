using DESystem.Data;
using DESystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DESystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly JWTConfig _jwtConfig;

        public AuthController(ILogger<AuthController> logger,
                        UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signinManager,
                        IOptions<JWTConfig> jwtConfig)
        {
            _logger = logger;
            _userManager = userManager;
            _signinManager = signinManager;
            _jwtConfig = jwtConfig.Value;
        }

        [HttpGet, Route("userlist")]
        public async Task<object> GetUserList()
        {
            try
            {
                var users = _userManager.Users;
                return await Task.FromResult(users);
            }catch(Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        [HttpPost, Route("register")]
        public async Task<object> Register([FromBody] RegisterModel model)
        {
            try
            {
                var user = new ApplicationUser()
                {
                    FullName = model.FullName,
                    UserName = model.Email,
                    DateCreated = DateTime.Now,
                    Email = model.Email,
                    DateModified = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return await Task.FromResult("User has been Registered successfully");
                }
                return await Task.FromResult(string.Join(",", result.Errors.Select(x => x.Description).ToArray()));
            }catch(Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        [HttpPost, Route("login")]
        public async Task<object> Login([FromBody] LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signinManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(model.Email);
                        var user = new UserModelDTO(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated);
                        user.Token = GenerateToken(appUser);
                        return await Task.FromResult(user);
                    }
                }
                return await Task.FromResult("invalid email and username");
            }catch(Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        private string GenerateToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
