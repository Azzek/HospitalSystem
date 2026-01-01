namespace HospitalSystem.Common
{
    public static class AppDefs
    {
        public enum UserRole
        {
            Admin,
            User,
        }

        public const int MIN_LENGTH = 2;
        public const int MAX_LENGTH = 64;

        public const string DB_CONNECTION_STRING = "Data source=database.db";
    }
}
