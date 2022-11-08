namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Doctor
    {
        private const string SEPARATOR = ", ";

        public Doctor(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }

        public String FullName
        {
            get
            {
                return FirstName + SEPARATOR + LastName;
            }
        }

        public String Specializtion { get; set; }

        public Hospital Hospial { get; set; }

    }
}