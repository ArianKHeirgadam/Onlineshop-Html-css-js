using System.ComponentModel.DataAnnotations;

namespace Onlineshopnew.Dto.Account
{
    public class LoginDto
    {
        [StringLength(64)]
        public string Username { get; set; } = null!;

        [StringLength(64)]
        public string Password { get; set; } = null!;
    }
}
