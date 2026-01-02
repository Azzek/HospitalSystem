using System;
using HospitalSystem.Models;
using HospitalSystem.Utils;
using HospitalSystem.Managers;

namespace HospitalSystem.Menus
{
    public class LoginMenu
    {
        public User ShowMenu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Please provide credentials to log in.");
                Console.WriteLine("====== Login Menu ======");

                string login = Validator.GetValidString("Username:");
                string password = Validator.GetValidString("Password:");

                User? logedUser = AuthManager.LogIn(login, password);

                if (logedUser != null)
                {
                    return logedUser;
                }

                Console.WriteLine("Wrong cregentials! Try again.");
                Util.Pause();
            }
        }

        
    }
}