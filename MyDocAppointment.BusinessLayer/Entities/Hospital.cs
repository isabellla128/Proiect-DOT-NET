using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Hospital
    {
        public Hospital(string name, string address, string phone)
        {
            Name = name;
            Address = address;
            Phone = phone;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }

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


