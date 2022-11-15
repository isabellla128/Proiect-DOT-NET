using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PrescriptionRepository : Repository<Prescription>
    {
        public PrescriptionRepository(MyDocAppointmentDatabaseContext context) : base(context)
        {
        }
    }
}
