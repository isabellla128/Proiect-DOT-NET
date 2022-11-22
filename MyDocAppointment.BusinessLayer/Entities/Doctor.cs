namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Doctor
    {
        private const string SEPARATOR = ", ";

        public Doctor(string firstName, string lastName, string specialization, string email, string phone)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            Email = email; 
            Phone = phone;
            Patients = new List<Patient>();
        }

        public Guid Id { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Specialization { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public Hospital Hospial { get; private set; }

        public Guid HospitalId { get; private set; }

        public ICollection<Patient> Patients { get; private set; }

        public string FullName
        {
            get
            {
                return FirstName + SEPARATOR + LastName;
            }
        }

        public void AddHospitalToDoctor(Hospital hospital)
        {
            this.Hospial = hospital;
            HospitalId = hospital.Id;
        }

        public void AddRelatedPacient(Patient patient)
        {
            Patients.Add(patient);
        }
	public void AddRelatedPacient(Patient patient)
        {
            Patients.Add(patient);
        }

    }
}