using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        protected IDatabaseContext context;

        public AppointmentRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public Appointment Add(Appointment appointment)
        {

            context.Appointments.Add(appointment);
            return appointment;
        }

        public Appointment Delete(Guid id)
        {
            var entity = context.Appointments.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no Appointment with id = {id}");
            }
            context.Appointments.Remove(entity);
            return entity;
        }

        public IEnumerable<Appointment> Find(Expression<Func<Appointment, bool>> predicate)
        {
            return context.Appointments
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<Appointment> GetAll()
        {
            return context.Appointments.ToList();
        }

        public virtual Appointment? GetById(Guid id)
        {
            return context.Appointments.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual Appointment Update(Appointment entity)
        {
            context.Appointments.Update(entity);
            return entity;
        }
    }
}
