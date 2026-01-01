using HospitalSystem.Models;

namespace HospitalSystem.Managers
{
    public static class UserManager
    {
        public static bool Add(User user)
        {   
            DatabaseManager.CreateUser(user);
            return true;
            // later here will be validation and other logic.
        }
        
        public static bool Delete(int id)
        {   
            
            return true;
        }

        public static User? Get(int id)
        {
            return null;
        }

        public static bool GetAll()
        {
            return true;
        }

        public static bool Update(User user)
        {
            return true;
        }
    }
}