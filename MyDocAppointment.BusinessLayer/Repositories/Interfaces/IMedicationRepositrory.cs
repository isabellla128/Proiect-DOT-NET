using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IMedicationRepositrory
    {
        Medication Add(Medication medication);
        Medication Delete(Guid id);
        IEnumerable<Medication> Find(Expression<Func<Medication, bool>> predicate);
        IEnumerable<Medication> GetAll();
        Medication? GetById(Guid id);
        void SaveChanges();
        Medication Update(Medication entity);
    }
}