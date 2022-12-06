using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Appointment Add(Appointment appointment);
        Appointment Delete(Guid id);
        IEnumerable<Appointment> Find(Expression<Func<Appointment, bool>> predicate);
        IEnumerable<Appointment> GetAll();
        Appointment? GetById(Guid id);
        void SaveChanges();
        Appointment Update(Appointment entity);
    }
}