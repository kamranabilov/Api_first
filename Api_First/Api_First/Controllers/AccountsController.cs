using Api_First.DTOs.Account;
using Api_First.Models;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IConfiguration _config;
        //, IConfiguration config

        public AccountsController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager/*IConfiguration config*/)
        {
            _userManager = userManager;
            _roleManager = roleManager;
           // _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.Username);
            if (user != null) return BadRequest();
            user = new AppUser
            {
                UserName = dto.Username,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname
            };
            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            IdentityResult resultRole = await _userManager.AddToRoleAsync(user, "Member");
            if (!resultRole.Succeeded) return BadRequest(resultRole.Errors);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.Username);
            if (user is null) return NotFound();

            var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!result)
                return BadRequest(new 
                {
                    code = "password or Username",
                    description = "Pass or UserN incorrect"
                });
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.UserData,user.UserName)
            };

            IList<string> roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim("Role", r)));

            string keyStr = "8dbf1ecb-a05e-403c-b64a-9bf78972eee9";

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));

            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://localhost:44343/",
                audience: "https://localhost:44343/",
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(3),
                claims:claims
                );

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(tokenStr);
        }

        [HttpGet("test")]
        //[Authorize/*(Roles ="Member")*/]
        public async Task<IActionResult> Test()
        {
            AppUser user = await _userManager.FindByNameAsync("Leo");
            return Ok(user);
        }
        //[HttpGet]
        //[Route("create")]
        //public async Task<IActionResult> CreateRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("Member"));
        //    await _roleManager.CreateAsync(new IdentityRole("Moderator"));
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    return NoContent();
        //}
    }
}
