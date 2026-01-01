using System;
using HospitalSystem.Common;
using HospitalSystem.Models;
using HospitalSystem.Utils;

namespace HospitalSystem.Menus
{
    public class MainMenu
    {
        public bool ShowMenu(User user)
        {
            bool isLoggdIn = true;

            while(isLoggdIn)
            {
                Console.Clear();
                RenderMenu(user);

                int choice = Validator.GetValidInt("Choice:");

                switch (choice)
                {
                    case 0:
                        return false;
                    case 1:
                        return true;
                    case 2:
                        PatientsMenu.ShowMenu(user.Role);
                        break;
                    case 3:
                        // DoctorsMenu.ShowMenu(user.Role);
                        break;
                    case 4:
                        // VisitsMenu.ShowMenu(user.Role);
                        break;
                    case 5 when user.Role == AppDefs.UserRole.Admin:
                        UsersMenu.ShowMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Util.Pause();
                        break;
                }
            }
            return true;
        }
        
        private void RenderMenu(User user)
        {
                Console.WriteLine("=== Main Menu ===");
                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Logout");
                Console.WriteLine("2. Patients");
                Console.WriteLine("3. Doctors");
                Console.WriteLine("4. Vists");

                if (user.Role == AppDefs.UserRole.Admin)
                {
                    Console.WriteLine("5. Users");
                }
                
                Console.WriteLine("0. Exit program");
        }
    }
}