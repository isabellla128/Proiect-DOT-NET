using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PatientRepository : Repository<Patient>
    {
        public PatientRepository(TestsDatabaseContext context) : base(context)
        {
        }
    }
}
