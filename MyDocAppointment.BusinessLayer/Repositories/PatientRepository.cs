using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Net.Sockets;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MyDocAppointmentDatabaseContext context;

        public PatientRepository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }

        public Patient? GetById(Guid id)
        {
            return this.context.Patients.FirstOrDefault(patient => patient.Id == id);
        }

        public IEnumerable<Patient> GetAll()
        {
            return this.context.Patients.ToList();
        }

        public void Add(Patient patient)
        {
            this.context.Patients.Add(patient);
            this.context.SaveChanges();
        }
        public void Update(Patient patient)
        {
            this.context.Update(patient);
            this.context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var patient = this.context.Patients.FirstOrDefault(patient => patient.Id == id);
            if (patient == null)
            {
                throw new ArgumentException($"There is no patient with given id: {id}");
            }
            this.context.Patients.Remove(patient);
            this.context.SaveChanges();
        }
    }
}
