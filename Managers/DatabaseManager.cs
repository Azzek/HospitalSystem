using System.Data;
using Microsoft.Data.Sqlite;

using HospitalSystem.Common;
using HospitalSystem.Models;

namespace HospitalSystem.Managers
{
    public static class DatabaseManager
    {
        private static readonly SqliteConnection _sqliteConnection = new SqliteConnection(AppDefs.DB_CONNECTION_STRING);

        public static void Init()
        {
            CreateDB();
        }

        //====== Users ======
        public static void CreateUser(User user)
        {
            OpenConnection();

            var command = _sqliteConnection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Users (Username, Password, Email, Role)
                VALUES ($username, $password, $email, $role);
            ";

            command.Parameters.AddWithValue("username", user.UserName);
            command.Parameters.AddWithValue("password", user.Password);
            command.Parameters.AddWithValue("email", user.Email);
            command.Parameters.AddWithValue("role", (int)user.Role);

            command.ExecuteNonQuery();
            CloseConnection();
        }
        public static User? GetUser(string userName, string password)
        {
            OpenConnection();

            var command = _sqliteConnection.CreateCommand();
            command.CommandText = @"
                SELECT Id, Username, Email, Role
                FROM Users
                WHERE Username = $userName AND Password = $password
                ";

            command.Parameters.AddWithValue("$userName", userName);
            command.Parameters.AddWithValue("$password", password);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var userId = reader.GetInt32(reader.GetOrdinal("Id"));
                    var name = reader.GetString(reader.GetOrdinal("Username"));
                    var email = reader.GetString(reader.GetOrdinal("Email"));
                    var role = (AppDefs.UserRole)reader.GetInt16(reader.GetOrdinal("Role"));

                    CloseConnection();
                    return new User(userId,"", name, email, role);
                }
            }

            CloseConnection();
            return null;
        }

        public static bool DeleteUser(int id)
        {
            return true;
        }

        //============ PATIENTS ============

        public static Patient? GetPatient(int id)
        {
            OpenConnection();
            var command = _sqliteConnection.CreateCommand();

            command.CommandText = @"
                SELECT FirstName, LastName, DateOfBirth, Phone 
                FROM Patients
                WHERE Id = $id
            ";
            command.Parameters.AddWithValue("id", id);

            using(var reader = command.ExecuteReader())
            {   
                while (reader.Read())
                {
                    var patientId = reader.GetInt32(reader.GetString(reader.GetOrdinal("Id")));
                    var firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    var lastName = reader.GetString(reader.GetOrdinal("LastName"));
                    var dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    var phone = reader.GetString(reader.GetOrdinal("Phone"));
                    

                    CloseConnection();
                    return new Patient(patientId, firstName, lastName, dateOfBirth, phone);
                }
                
                CloseConnection();
                return null;
            }
        }

        public static bool AddPatient(Patient patient)
        {
            OpenConnection();

            var command = _sqliteConnection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Patients (Firstname, Lastname, DateOfBirth, Phone)
                VALUES ($firstName, $lastName, $dateOfBirth, $phone);
            ";

            command.Parameters.AddWithValue("firstName", patient.FirstName);
            command.Parameters.AddWithValue("lastName", patient.LastName);
            command.Parameters.AddWithValue("dateOfBirth", patient.DateOfBirth);
            command.Parameters.AddWithValue("phone", patient.PhoneNumber);

            command.ExecuteNonQuery();
            CloseConnection();

            return true;
        }

        public static bool DeletePatient(int id)
        {
            OpenConnection();
            var command = _sqliteConnection.CreateCommand();

            command.CommandText = @"
                DELETE FROM Patients
                WHERE Id = $id;";
            command.Parameters.AddWithValue("id", id);

            int rowsDeleted = command.ExecuteNonQuery();

            CloseConnection();  
            return rowsDeleted > 0;
        }

        public static bool Update(Patient patient)
        {
            return true;
        }

        public static bool GetAll(int id)
        {
            return true;
        }

        //============ PATIENTS ============
        private static void CreateDB()
        {
            OpenConnection();

            var command = _sqliteConnection.CreateCommand();

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY,
                    Username TEXT NOT NULL,
                    Password TEXT NOT NULL,
                    Email TEXT NOT NULL,
                    Role INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Patients (
                    Id INTEGER PRIMARY KEY,
                    Firstname TEXT NOT NULL,
                    Lastname TEXT NOT NULL,
                    DateOfBirth TEXT NOT NULL,
                    Phone TEXT NOT NULL
                );

                INSERT INTO Users (Username, Password, Email, Role)
                VALUES ('admin', 'admin', 'admin@hospital.com', 1);
            ";

            command.ExecuteNonQuery();
            CloseConnection();
        }

        private static void OpenConnection()
        {
            if (_sqliteConnection.State != ConnectionState.Open)
            {
                _sqliteConnection.Open();
            }
        }

        private static void CloseConnection()
        {
            if (_sqliteConnection.State != ConnectionState.Closed)
            {
                _sqliteConnection.Close();
            }
        }


    }
}
