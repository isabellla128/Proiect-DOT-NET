namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Doctor
    {
        private const string SEPARATOR = ", ";

        public Doctor(string firstName, string lastName, string specialization, string email, string phone, string title, string profession, string location, double grade, int reviews)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            Email = email; 
            Phone = phone;
            Title = title;
            Profession = profession;
            Location = location;
            Grade = grade;
            Reviews = reviews;

            Patients = new List<Patient>();
        }

        public Guid Id { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Specialization { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }
        public string Title { get; private set; }
        public string Profession { get; private set; }
        public string Location { get; private set; }
        public double Grade { get; private set; }

        public int Reviews { get; private set; }



        public Hospital? Hospial { get; private set; }

        public Guid? HospitalId { get; private set; }

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

    }
}