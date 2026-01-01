using HospitalSystem.Common;

namespace HospitalSystem.Models
{
    public class User
    {
        public int Id {get; set;}
        public string UserName { get; set; } = string.Empty;
        public string Password {get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public AppDefs.UserRole Role { get; set; } = AppDefs.UserRole.User;

        public User(int id, string userName, string password, string email, AppDefs.UserRole userRole)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Email = email;
            Role = userRole;
        }
    }
}
