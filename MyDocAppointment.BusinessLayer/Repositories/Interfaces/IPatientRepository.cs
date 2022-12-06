using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Patient Add(Patient patient);
        Patient Delete(Guid id);
        IEnumerable<Patient> Find(Expression<Func<Patient, bool>> predicate);
        IEnumerable<Patient> GetAll();
        Patient? GetById(Guid id);
        void SaveChanges();
        Patient Update(Patient entity);
    }
}