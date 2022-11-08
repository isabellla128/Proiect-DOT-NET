using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Numerics;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly MyDocAppointmentDatabaseContext context;
        public AppointmentRepository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }
        public void Add(Appointment appointment)
        {
            context.Appointments.Add(appointment);
            context.SaveChanges();
        }
        public void Update(Appointment appointment)
        {
            context.Update(appointment);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var appointment = this.context.Appointments.FirstOrDefault(appointment => appointment.Id == id);
            if (appointment == null)
            {
                throw new ArgumentException($"There is no appointment with given id: {id}");
            }
            context.Appointments.Remove(appointment);
            context.SaveChanges();
        }
        public Appointment? GetById(int id)
        {
            return context.Appointments.FirstOrDefault(c => c.Id == id);
        }
        public IEnumerable<Appointment> GetAll()
        {
            return context.Appointments.ToList();
        }
    }
}
