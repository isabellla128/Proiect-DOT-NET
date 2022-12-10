using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IMedicationDosageHistoryRepository
    {
        MedicationDosageHistory Add(MedicationDosageHistory medicationDosageHistory);
        MedicationDosageHistory Delete(Guid id);
        IEnumerable<MedicationDosageHistory> Find(Expression<Func<MedicationDosageHistory, bool>> predicate);
        IEnumerable<MedicationDosageHistory> GetAll();
        MedicationDosageHistory? GetById(Guid id);
        void SaveChanges();
        MedicationDosageHistory Update(MedicationDosageHistory entity);
    }
}