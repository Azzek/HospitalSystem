using System;
using HospitalSystem.Common;
using HospitalSystem.Managers;
using HospitalSystem.Models;
using HospitalSystem.Utils;

namespace HospitalSystem.Menus
{
    public class UsersMenu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                RenderMenu(AppDefs.UserRole.Admin);

                int choice = Validator.GetValidInt("Choice:");

                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        GetUser();
                        break;
                    case 2:
                        AddUser();
                        break;
                    case 3:
                        // Update user
                        break;
                    case 4:
                        // Delete user
                        break;
                    case 5:
                        // Get all users
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Util.Pause();
                        break;
                }
            }   
        }

        private static void GetUser()
        {
            Console.Clear();
            Console.WriteLine("====== Get User Details ======");

            int userId = Validator.GetValidInt("Provide user ID:");

            User? user = UserManager.Get(userId);

            if (user == null)
            {
                Console.WriteLine($"User with ID {userId} not found.");
                Util.Pause();
                return;
            }

            DisplayUserDetails(user);
            Util.Pause();
        }

        private static void AddUser()
        {
            Console.Clear();
            Console.WriteLine("====== Add New User ======");

            string username = Validator.GetValidString("Provide username:");
            string password = Validator.GetValidString("Provide password:");
            string email = Validator.GetValidString("Provide email:");
            
            User newUser = new User(0, username, password, email, AppDefs.UserRole.User);

            Console.Clear();
            Console.WriteLine("You are about to add the following user:");
            DisplayUserDetails(newUser);

            if (!Util.IsContinue("Are you sure you want to add this user?"))
            {
                Console.WriteLine("User addition was cancelled.");
                Util.Pause();
                return;
            }
            
            UserManager.Add(newUser);
            Console.WriteLine("User added successfully.");
            Util.Pause();
        }

        public static void DeleteUser()
        {
            Console.Clear();
            Console.WriteLine("====== Delete User ======");

            int userId = Validator.GetValidInt("Provide user ID to delete:");

            if (!Util.IsContinue($"Are you sure you want to delete user with ID {userId}?"))
            {
                Console.WriteLine("User deletion was cancelled.");
                Util.Pause();
                return;
            }

            if (UserManager.Delete(userId))
            {
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete user. Please try again.");
            }

            Util.Pause();
        }

        private static void RenderMenu(AppDefs.UserRole role)
        {
            Console.WriteLine("=== Patients Menu ===");
            Console.WriteLine("1. Get patient details");
            Console.WriteLine("2. Add patient");
            Console.WriteLine("3. Update patient");
            Console.WriteLine("4. Delete patient");
            Console.WriteLine("5. Get all patients");
            Console.WriteLine("0. Back");
        }

        private static void DisplayUserDetails(User user)
        {
            Console.WriteLine();
            Console.WriteLine("=== User Details ===");
            Console.WriteLine($"User ID: {user.Id}");
            Console.WriteLine($"Username: {user.UserName}");
            Console.WriteLine($"Password: {user.Password}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Role: {user.Role}");
        }
    }
}