using System.Security.Principal;

namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Hospital
    {
        public Hospital(string name, string address, string phone)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Doctors = new List<Doctor>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }

        public ICollection<Doctor> Doctors { get; set; }
        public bool Validate()
        {
            var isValid = true;
            if (string.IsNullOrWhiteSpace(Name))
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(Address))
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(Phone))
            {
                isValid = false;
            }
            return isValid;
        }

    }
}


