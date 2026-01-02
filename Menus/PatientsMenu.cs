using System;
using System.Collections.Generic;
using HospitalSystem.Common;
using HospitalSystem.Managers;
using HospitalSystem.Models;
using HospitalSystem.Utils;

namespace HospitalSystem.Menus
{
    public static class PatientsMenu
    {
        public static void ShowMenu(AppDefs.UserRole role)
        {
            while (true)
            {
                Console.Clear();
                RenderMenu(role);

                string input = Validator.GetValidString("Choice:");
                bool vaidInput = int.TryParse(input, out int choice);

                if (!vaidInput)
                {
                    Console.WriteLine("Choice should be an integer in range 0-3");
                    Util.Pause();

                }

                switch (choice)
                {
                    case 1:
                        GetPatient();
                        break;
                    case 2:
                        AddPatient();
                        break;
                    case 3:
                        UpdatePatient();
                        break;
                    case 4 when role == AppDefs.UserRole.Admin:
                        DeletePatient();
                        break;
                    case 5 when role == AppDefs.UserRole.Admin:
                        GetAllPatients();
                        break;
                    default:
                        Console.WriteLine("Choice should be an integer in range 0-4");
                        break;
                }
            }
        }

        private static void RenderMenu(AppDefs.UserRole role)
        {
            Console.WriteLine("=== Patients Menu ===");
            Console.WriteLine("1. Get patient details");
            Console.WriteLine("2. Add patient");
            Console.WriteLine("3. Update patient");

            if (role == AppDefs.UserRole.Admin)
            {
                Console.WriteLine("4. Delete patient");
                Console.WriteLine("5. Get all patients");
            }

            Console.WriteLine("0. Back");
        }

        public static void DisplayPatientDetails(Patient patient)
        {
            Console.WriteLine("=== Patient Details ===");
            Console.WriteLine($"Patient ID: {patient.Id}");
            Console.WriteLine($"Name: {patient.FirstName} {patient.LastName}");
            Console.WriteLine($"Date of Birth: {patient.DateOfBirth::yyyymmdd}");
            Console.WriteLine($"Phone: {patient.PhoneNumber}");
        }

        private static void GetPatient()
        {
            Console.Clear();
            Console.WriteLine("====== Get Patient Details ======");

            int patientId = Validator.GetValidInt("Provide patient ID:");

            Patient? patient = PatientManager.Get(patientId);

            if (patient == null)
            {
                Console.WriteLine($"Patient with ID {patientId} not found.");
                Util.Pause();
                return;
            }

            DisplayPatientDetails(patient);
            Util.Pause();
            
        }

        private static void GetAllPatients()
        {
            int offset = 0;
            const int pageSize = 10;

            int totalPatients = PatientManager.GetPatientsCount();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== All Patients ======");

                var patients = PatientManager.GetAllPaginated(offset, pageSize);

                if (patients.Count == 0)
                {
                    Console.WriteLine("No patients found.");
                }
                else
                {
                    foreach (Patient patient in patients)
                    {
                        DisplayPatientDetails(patient);
                        Console.WriteLine("---------------------");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("1 - Next | 2 - Previous | 3 - Quit");

                int choice = Validator.GetValidInt("Select:");

                switch (choice)
                {
                    case 1:
                        if (offset + pageSize < totalPatients)
                            offset += pageSize;
                        break;

                    case 2:
                        if (offset - pageSize >= 0)
                            offset -= pageSize;
                        break;

                    case 3:
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Util.Pause();
                        break;
                }
            }
        }


        private static void AddPatient()
        {
            Console.Clear();
            Console.WriteLine("=== Add patient ===");

            while (true)
            {
                string firstName = Validator.GetValidString("Provide patient name:");
                string lastName = Validator.GetValidString("Provide patient last name:");
                DateTime dateOfBirth = Validator.GetValidDateTime("Provide patient date of birth:");
                string phone = Validator.GetValidString("Provide patient phone:");

                Console.Clear();
                Patient newUser = new Patient(0, firstName, lastName, dateOfBirth, phone);

                Console.WriteLine("You are about to add the following patient:");
                DisplayPatientDetails(newUser);

                if (!Util.IsContinue("Are you sure you want to add this patient?"))
                {
                    Console.WriteLine("Patient addition was cancelled.");
                    Util.Pause();
                    return;
                }

                if (PatientManager.Add(newUser))
                {
                    Console.WriteLine("Patient was successfully added.");
                    break;
                }
                else
                {
                    Console.WriteLine("Failed to add patient. Please try again.");
                }
            }
        }
        public static void DeletePatient()
        {
            Console.Clear();
            Console.WriteLine("====== Delete Patient ======");

            string input = Validator.GetValidString("Provide patient id:");
            
            if (!int.TryParse(input, out int id) || id <= 0)
            {
                Console.WriteLine("ID should be a positive integer.");
                Util.Pause();
                return;
            }

            bool isDeleted = PatientManager.Delete(id);

            if (!isDeleted)
            {
                Console.WriteLine("Wrong ID, no patient was deleted.");
            }
            else
            {
                Console.WriteLine("Patient was successfully deleted.");
            }
            
            Util.Pause();
        }

        public static void UpdatePatient()
        {
            Console.Clear();
            Console.WriteLine("====== Update Patient ======");
            
            string input = Validator.GetValidString("Provide patient id:");
            
            if (!int.TryParse(input, out int id) || id <= 0)
            {
                Console.WriteLine("ID should be a positive integer.");
                Util.Pause();
                return;
            }

            Patient? patientToUpdate = PatientManager.Get(id);

            if (patientToUpdate == null)
            {
                Console.WriteLine("Patient not found.");
                Util.Pause();
                return;
            }

            DisplayPatientDetails(patientToUpdate);

            Console.WriteLine("Select which field you would like to edit:");
            Console.WriteLine("1. First name.");
            Console.WriteLine("2. Last name.");
            Console.WriteLine("3. Date of birth.");
            Console.WriteLine("4. Phone number.");

            string choiceInput = Validator.GetValidString("Choice:");
            if (!int.TryParse(choiceInput, out int choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Choice should be an integer in range 1-4.");
                Util.Pause();
                return;
            }

            switch (choice)
            {
                case 1:
                    string newFirstName = Validator.GetValidString("Provide new first name:");
                    patientToUpdate.FirstName = newFirstName;
                    break;
                case 2:
                    string newLastName = Validator.GetValidString("Provide new last name:");
                    patientToUpdate.LastName = newLastName;
                    break;
                case 3:
                    DateTime newDateOfBirth = Validator.GetValidDateTime("Provide new date of birth:");
                    patientToUpdate.DateOfBirth = newDateOfBirth;
                    break;
                case 4:
                    string newPhoneNumber = Validator.GetValidString("Provide new phone number:");
                    patientToUpdate.PhoneNumber = newPhoneNumber;
                    break;
            }


            bool isUpdated = PatientManager.Update(patientToUpdate);

            if (!isUpdated)
            {
                Console.WriteLine("Wrong ID, no patient was updated.");
            }
            else
            {
                Console.WriteLine("Patient was successfully updated.");
            }
            
            Util.Pause();
        }
    }
}