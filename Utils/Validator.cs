using System;

namespace HospitalSystem.Utils
{
    public static class Validator
    {
        public static string GetValidString(string prompt)
        {
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine()?.Trim() ?? "";

            }
            while (input.Length < Common.AppDefs.MIN_LENGTH && input.Length > Common.AppDefs.MAX_LENGTH);
            
            return input;
        }

        public static int GetValidInt(string prompt)
        {
            string input = Validator.GetValidString(prompt);

            if (!int.TryParse(input, out int choice) && choice < 0)
            {
                Console.WriteLine("Choice must be a real number.");
                Util.Pause();
                return -1;
            }

            return choice;
        }

        public static DateTime GetValidDateTime(string prompt)
        {
            DateTime date;

            while (true)
            {
                string input = GetValidString(prompt);

                if (DateTime.TryParse(input, out date))
                    return date;

                Console.WriteLine("Invalid date format. Please try again.");
            }
        }
        
    }
}