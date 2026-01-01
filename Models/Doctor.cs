namespace HospitalSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;

        public Doctor(int id, string name, string specialty)
        {
            Id = id;
            Name = name;
            Specialty = specialty;
        }
    }
}