using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MyDocAppointmentDatabaseContext context;

        public void Add(Doctor doctor)
        {
            this.context.Add(doctor);
            this.context.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            this.context.Update(doctor);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctor = this.context.Doctors.FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                throw new ArgumentException($"There is no doctor with given id: {id}");
            }
            this.context.Doctors.Remove(doctor);
            this.context.SaveChanges();
        }

        public Doctor? GetById(int id)
        {
            return context.Doctors.FirstOrDefault(d => d.Id == id);
        }
        public IEnumerable<Doctor> GetAll()
        {
            return context.Doctors.ToList();
        }
    }
}