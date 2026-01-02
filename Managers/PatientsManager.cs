using System;
using System.Collections.Generic;
using HospitalSystem.Models;

namespace HospitalSystem.Managers
{
    public static class PatientManager
    {
        public static bool Add(Patient patient)
        {
            return DatabaseManager.AddPatient(patient);
            // later here will be validation and other logic.
        }
        
        public static bool Delete(int id)
        {
            return true;
        }

        public static Patient? Get(int id)
        {
            return DatabaseManager.GetPatient(id);
        }

        public static List<Patient> GetAllPaginated(int offset, int pageSize)
        {
            return DatabaseManager.GetAllPatientsPaginated(offset, pageSize);
        }

        public static bool Update(Patient patient)
        {
            return true;
        }

        public static int GetPatientsCount()
        {
            return DatabaseManager.GetPatientsCount();
        }
    }
}