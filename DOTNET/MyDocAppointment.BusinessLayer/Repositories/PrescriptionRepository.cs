using Microsoft.EntityFrameworkCore;
using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PrescriptionRepository : Repository<Prescription>
    {
        public PrescriptionRepository(MyDocAppointmentDatabaseContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyCollection<Prescription>> GetAll()
        {
            return await context.Prescriptions.Include(p => p.MedicationDosagePrescriptions).ToListAsync();

        }
    }
}
