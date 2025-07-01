using Onlineshopnew.Models;

namespace Onlineshopnew.Dto.User
{
    public class AddUserDto
    {
        public string Name { get; set; }

        public string? Tell { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }
        public virtual TblUser totbl()
        {
            return new TblUser
            {
                Name = Name,
                Tell = Tell,
                Username = Username,
                Password = Password,
                RoleId = RoleId
            };
        }
    }
}
