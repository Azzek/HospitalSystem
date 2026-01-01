using HospitalSystem.Managers;
using HospitalSystem.Menus;
using HospitalSystem.Models;

namespace HospitalSystem
{
    public class Program
    {
        private static readonly LoginMenu  _loginMenu = new LoginMenu();
        private static readonly MainMenu _mainMenu = new MainMenu();
        private static User? _loggedUser;
        private static bool running = true;

        public static void Main(string[] args)
        {
            DatabaseManager.Init();  
            while (true)
            {
                var logedUser = _loginMenu.ShowMenu();
                bool confirm = _mainMenu.ShowMenu(logedUser);
                
            }
        }
    }
}
