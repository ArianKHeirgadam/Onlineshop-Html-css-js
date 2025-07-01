using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onlineshopnew.Dto.User;
using Onlineshopnew.Models;

namespace Onlineshopnew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ContextDB _context;
        public UserController(ContextDB context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetUserDto>>> Get()
        {
            if (await _context.TblUsers.AnyAsync())
            {
                List<TblUser> users = await _context.TblUsers.Include(i => i.Role).ToListAsync();
                List<GetUserDto> dto = new List<GetUserDto>();
                users.ForEach(i => dto.Add(new GetUserDto(i)));
                return Ok(dto);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetByID(int id)
        {
            var users = await _context.TblUsers.Include(i => i.Role).FirstOrDefaultAsync(i => i.Id == id);
            if (users == null) return NotFound();
            return Ok(new GetUserDto(users));

        }

        [HttpPost]
        public async Task<ActionResult<GetUserDto>> Add(AddUserDto dto)
        {
            var user = dto.totbl();
            await _context.TblUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return Created("api/user", new GetUserDto(user));
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] EditUserDto dto)
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(x => x.Username == dto.Username);
            if (user == null) return NotFound();
            dto.ApplyTo(user);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> Delete(string username)
        {
            var u = await _context.TblUsers.FindAsync(username);
            if (u == null) return NotFound();
            _context.Remove(u);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
