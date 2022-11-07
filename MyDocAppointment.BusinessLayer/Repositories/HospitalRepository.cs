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
    public class HospitalRepository
    {
        private readonly MyDocAppointmentDatabaseContext context;
        public HospitalRepository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Hospital hospital)
        {
            context.Hospitals.Add(hospital);
            context.SaveChanges();
        }

        public void Update(Hospital hospital)
        {
            context.Update(hospital);
            context.SaveChanges();
        }
        public void Delete(Hospital hospital)
        {
            context.Hospitals.Remove(hospital);
            context.SaveChanges();
        }
        public Hospital? GetById(int id)
        {
            return context.Hospitals.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Hospital> GetAll()
        {
            return context.Hospitals.ToList();
        }
    }
}
