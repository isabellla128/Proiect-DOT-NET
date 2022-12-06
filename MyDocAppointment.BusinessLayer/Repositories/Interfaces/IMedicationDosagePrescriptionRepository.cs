using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IMedicationDosagePrescriptionRepository
    {
        MedicationDosagePrescription Add(MedicationDosagePrescription MedicationDosagePrescription);
        MedicationDosagePrescription Delete(Guid id);
        IEnumerable<MedicationDosagePrescription> Find(Expression<Func<MedicationDosagePrescription, bool>> predicate);
        IEnumerable<MedicationDosagePrescription> GetAll();
        MedicationDosagePrescription? GetById(Guid id);
        void SaveChanges();
        MedicationDosagePrescription Update(MedicationDosagePrescription entity);
    }
}