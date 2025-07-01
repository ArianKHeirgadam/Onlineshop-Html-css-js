using Onlineshopnew.Models;
using System.ComponentModel.DataAnnotations;

namespace Onlineshopnew.Dto.Account
{
    public class RegisterDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [StringLength(16)]
        public string? Tell { get; set; }
        [Required]
        [StringLength(64)]
        public string Username { get; set; } = null!;
        [Required]
        [StringLength(64)]
        public string Password { get; set; } = null!;
        public TblUser ToTbl()
        {
            return new TblUser
            {
                Name = Name,
                Tell = Tell,
                Username = Username,
                Password = Password,
                RoleId = 3
            };
        }

    }
}
