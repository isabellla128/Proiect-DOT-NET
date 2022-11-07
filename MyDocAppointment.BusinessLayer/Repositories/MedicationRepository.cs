using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class MedicationRepository
    {
        private readonly MyDocAppointmentDatabaseContext context;

        public MedicationRepository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }

        //queries
        public Medication? GetById(int id)
        {
            return this.context.Medications.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Medication> GetAll()
        {
            return this.context.Medications.ToList();
        }

        //commands

        public void Add(Medication medication)
        {
            this.context.Medications.Add(medication);
            this.context.SaveChanges();
        }

        public void Update(Medication medication)
        {
            this.context.Update(medication);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var medication = this.context.Medications.FirstOrDefault(c => c.Id == id);
            this.context.Medications.Remove(medication);
            this.context.SaveChanges();
        }

    }
}

