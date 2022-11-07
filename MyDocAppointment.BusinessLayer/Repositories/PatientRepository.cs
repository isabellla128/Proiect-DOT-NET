using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MyDocAppointmentDatabaseContext context;

        public PatientRepository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }

        public Patient? GetById(int id)
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

        public void Delete(int id)
        {
            var patient = this.context.Patients.FirstOrDefault(patient => patient.Id, id);
            this.context.Patients.Remove(patient);
            this.context.Patients.Remove(GetById(id));
        }
    }
}
