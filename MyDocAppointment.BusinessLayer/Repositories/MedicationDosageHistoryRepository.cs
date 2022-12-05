using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class MedicationDosageHistoryRepository : Repository<MedicationDosageHistory>
    {
        public MedicationDosageHistoryRepository(TestsDatabaseContext context) : base(context)
        {
        }
    }
}
