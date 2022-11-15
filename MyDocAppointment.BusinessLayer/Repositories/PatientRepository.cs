using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PatientRepository : Repository<Patient>
    {
        public PatientRepository(MyDocAppointmentDatabaseContext context) : base(context)
        {
        }
    }
}
