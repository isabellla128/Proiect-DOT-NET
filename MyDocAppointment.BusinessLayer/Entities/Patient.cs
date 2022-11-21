
namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Patient
    {
        public Patient(string firstName, string lastName, string email, string phone)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Doctors = new List<Doctor>();
        }

        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public ICollection<Doctor> Doctors { get; private set; }

    }

}
