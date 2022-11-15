using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly MyDocAppointmentDatabaseContext context;
        public PrescriptionRepository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }

        //queries
        public Prescription? GetById(Guid id)
        {
            return this.context.Prescriptions.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Prescription> GetAll()
        {
            return this.context.Prescriptions.ToList();
        }

        //commands

        public void Add(Prescription prescription)
        {
            this.context.Prescriptions.Add(prescription);
            this.context.SaveChanges();
        }

        public void Update(Prescription prescription)
        {
            this.context.Update(prescription);
            this.context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var prescription = this.context.Prescriptions.FirstOrDefault(c => c.Id == id);
            if (prescription == null)
            {
                throw new ArgumentException($"There is no prescription with given id: {id}");
            }
            this.context.Prescriptions.Remove(prescription);
            this.context.SaveChanges();
        }

        public void AddMedication(Guid id, Medication medication)
        {
            var prescription = this.context.Prescriptions.FirstOrDefault(c => c.Id == id);
            if (prescription == null)
            {
                throw new ArgumentException($"There is no prescription with given id: {id}");
            }
            prescription.Medications.Add(medication);
            this.context.Update(prescription);
            this.context.SaveChanges();
        }
    }
}
