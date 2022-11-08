namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Doctor
    {
        private const string SEPARATOR = ", ";

        public Doctor()
        {

        }
        public Doctor(string firstName, string lastName, string specialization)
        {
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
        }

        public int Id { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string FullName
        {
            get
            {
                return FirstName + SEPARATOR + LastName;
            }
        }

        public string Specialization { get; private set; }

        public Hospital Hospial { get; set; }

    }
}