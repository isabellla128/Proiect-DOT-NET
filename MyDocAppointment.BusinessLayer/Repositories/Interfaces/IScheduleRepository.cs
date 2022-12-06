using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        Schedule Add(Schedule Schedule);
        Schedule Delete(Guid id);
        IEnumerable<Schedule> Find(Expression<Func<Schedule, bool>> predicate);
        IEnumerable<Schedule> GetAll();
        Schedule? GetById(Guid id);
        void SaveChanges();
        Schedule Update(Schedule entity);
    }
}