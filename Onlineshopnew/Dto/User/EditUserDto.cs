using Onlineshopnew.Models;

namespace Onlineshopnew.Dto.User
{
    public class EditUserDto
    {
        public string Username { get; set; }
    
        public string Name { get; set; }
        public string? Tell { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public TblUser ApplyTo(TblUser user)
        {
            user.Name = Name;
            user.Tell = Tell;
            user.Password = Password;
            user.RoleId = RoleId;
            return user;
        }
    }
}
