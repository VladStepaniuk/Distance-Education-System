using DESystem.Data;
using DESystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ILogger<UserController> logger, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpGet, Route("userlist")]
        [Authorize (Roles = "Admin")]
        public async Task<object> GetUserList()
        {
            try
            {
                var users = _userManager.Users;
                List<UserModelDTO> allUsersDto = new List<UserModelDTO>();
                foreach(var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    allUsersDto.Add(new UserModelDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role));
                }
                return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Ok, "", allUsersDto));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Fail, ex.Message, null));
            }
        }

        [HttpGet, Route("getuser")]
        [Authorize(Roles = "User")]
        public async Task<object> GetUser()
        {
            try
            {
                var users = _userManager.Users;
                List<UserModelDTO> allUsersDto = new List<UserModelDTO>();
                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    if(role == "User")
                        allUsersDto.Add(new UserModelDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role));
                }
                return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Ok, "", allUsersDto));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Fail, ex.Message, null));
            }
        }

        [HttpGet, Route("getroleslist")]
        [Authorize(Roles = "User")]
        public async Task<object> GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles.Select(x => x.Name).ToList();
                return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Ok, "", roles));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Fail, ex.Message, null));
            }
        }

        [HttpPost ("addrole")]
        public async Task<object> AddRole([FromBody] AddRoleBindingModel model)
        {
            try
            {
                if(model == null || model.Role == "")
                {
                    return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Fail, "Parameter are missing", null));
                }
                if(await _roleManager.RoleExistsAsync(model.Role))
                {
                    return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Fail, "Role already exists", null));
                }
                var role = new IdentityRole();
                role.Name = model.Role;
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Ok, "Role added successfully", null));
                }
                return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Fail, "Something went wrong. Please, try again later", null));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new ResponseModel(Enums.ResponseCode.Fail, ex.Message, null));
            }
        }
    }
}
