using DESystem.Data;
using DESystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public AuthController(ILogger<AuthController> logger,
                        UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signinManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signinManager = signinManager;
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
                    return await Task.FromResult("Parameters are missing");


                    var result = await _signinManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return await Task.FromResult("login successfully");
                    }
                }
                return await Task.FromResult("invalid email and username");
            }catch(Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }
    }
}
