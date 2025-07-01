using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Onlineshopnew.Dto.Account;
using Onlineshopnew.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Onlineshopnew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ContextDB _context;

        public AccountController(ContextDB context)
        {
            _context = context;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.TblUsers.AnyAsync(i => i.Username == dto.Username))
            {
                return BadRequest("This username already exists");
            }

            var user = dto.ToTbl();
            user.RoleId = 3;
            await _context.TblUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return Created();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.TblUsers.Include(i => i.Role).FirstOrDefaultAsync(i => i.Username == dto.Username);
            if (user == null) return BadRequest("Username or password is wrong");

          
            return user.Password == dto.Password ? Ok(GenerateJwtToken(user.Role.Name,user.Username,user.Id)) : BadRequest("Username or password is wrong");
        }
        [HttpGet("DetermineUserRole")]
        [Authorize]
        public async Task<ActionResult<string>> DetermineUserRole()
        {
            bool claimExists = HttpContext.User.Claims.Any(i => i.Type == ClaimTypes.Role);
            if (!claimExists)
                return Forbid();
            string value = HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Role).Value;
            if (value == "Admin")
            {
                return Ok("admin.html");
            }
            else
            {
                return Ok("main.html");
            }
        }
        private string GenerateJwtToken(string rolename, string username, int userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("137c514cc904eb0cc089aca19fdab93c68e859249a335331368c893818c64b91"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Role, rolename),
        new Claim(ClaimTypes.Name, username),
        new Claim("UserId", userId.ToString())
    };

            var token = new JwtSecurityToken(
                issuer: "Developer",
                audience: "User",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
