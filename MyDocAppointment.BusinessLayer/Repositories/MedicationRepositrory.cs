using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class MedicationRepositrory : Repository<Medication>
    {
        public MedicationRepositrory(MyDocAppointmentDatabaseContext context) : base(context)
        {
        }
    }
}
