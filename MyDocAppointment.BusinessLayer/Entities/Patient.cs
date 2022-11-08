
namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Patient
    {
        public Patient()
        {

        }

        public Patient(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public ICollection<Doctor> Doctors { get; set; }

    }

}
