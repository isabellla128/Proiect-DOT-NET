using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class AppointmentRepository
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
        public void Delete(Appointment appointment)
        {
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
