namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Doctor
    {
        private const string SEPARATOR = ", ";

        public Doctor(string firstName, string lastName, string specialization, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            Email = email; 
            Phone = phone;
        }

        public int Id { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Specialization { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public Hospital Hospial { get; set; }

        public int HospitalId { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + SEPARATOR + LastName;
            }
        }


    }
}