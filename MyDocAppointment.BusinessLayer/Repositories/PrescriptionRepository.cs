﻿using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PrescriptionRepository
    {
        private readonly MyDocAppointmentDatabaseContext context;
        public PrescriptionRepository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }

        //queries
        public Prescription? GetById(int id)
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

        public void Delete(int id)
        {
            var prescription = this.context.Prescriptions.FirstOrDefault(c => c.Id == id);
            this.context.Prescriptions.Remove(prescription);
            this.context.SaveChanges();
        }

        public void AddMedication(int id, Medication medication)
        {
            var prescription = this.context.Prescriptions.FirstOrDefault(c => c.Id == id);
            prescription.Medications.Add(medication);
            this.context.Update(prescription);
            this.context.SaveChanges();
        }
    }
}
