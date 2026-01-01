using System;

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

        public static bool GetAll()
        {
            return true;
        }

        public static bool Update(Patient patient)
        {
            return true;
        }
    }
}