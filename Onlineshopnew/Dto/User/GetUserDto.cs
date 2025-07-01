using Onlineshopnew.Models;

namespace Onlineshopnew.Dto.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tell { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public GetUserDto(TblUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Tell = user.Tell;
            Username = user.Username;
            Password = user.Password;
           
            RoleId = user.RoleId;
        }
    }
}
