using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OldForum.Interfaces;
using SharedEntities.Entities;
using SharedEntities.Models;

namespace OldForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<VozUser> _userManger;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserAccessor _userAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(IJwtGenerator jwtGenerator, UserManager<VozUser> userManger, IUserAccessor userAccessor, RoleManager<IdentityRole> roleManager)
        {
            _jwtGenerator = jwtGenerator;
            _userManger = userManger;
            _userAccessor = userAccessor;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> CreateUser([FromBody] Register user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var vozUser = new VozUser()
                {
                    Email = user.Email,
                    Name = user.Name,
                    UserName = user.Email,                    
                };
                var roleExist = await _roleManager.RoleExistsAsync("Admin");
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                var result = await _userManger.CreateAsync(vozUser, user.Password);
                var rs = await _userManger.AddToRoleAsync(vozUser, "Admin");
                if (result.Succeeded && rs.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> SignIn([FromBody] LoginUser login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var user = await _userManger.FindByEmailAsync(login.Email);
                if (user == null)
                {
                    return BadRequest("Email or password is incorrect");
                }

                var result = await _userManger.CheckPasswordAsync(user, login.Password);
                
                if (result == false)
                {
                    return BadRequest("Email or password is incorrect");
                }

                return Ok(new User
                {
                    Email = user.Email,
                    Name = user.Name,
                    Token = _jwtGenerator.CreateToken(user)
                }) ;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var current = await _userManger.FindByNameAsync(_userAccessor.GetCurrentUserName());

            if (current !=null)
            {
                var role = await _userManger.GetRolesAsync(current);

                return Ok(new User
                {
                    Email = current.Email,
                    Name = current.Name,
                    Role = role,
                });
            }
            return BadRequest();
        }

    }
}