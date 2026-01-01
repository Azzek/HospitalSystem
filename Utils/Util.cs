using System;

namespace HospitalSystem.Utils
{
    public class Util
    {
        public static void Pause()
        {
            Console.WriteLine("Click any key...");
            Console.ReadKey();
        }
        public static bool IsContinue(string prompt)
        {   
            Console.WriteLine(prompt);
            Console.WriteLine("Press 'y' to continue or any key to cancel...");
            var key = Console.ReadKey();
            return key.KeyChar == 'y' || key.KeyChar == 'Y';
        }
    }
}