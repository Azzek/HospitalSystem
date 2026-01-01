using HospitalSystem.Models;

namespace HospitalSystem.Managers
{
    public static class AuthManager
    {
        public static User? LogIn(string userName, string password)
        {
            User? user = DatabaseManager.GetUser(userName, password);
            
            return user;
        }
    }
}
