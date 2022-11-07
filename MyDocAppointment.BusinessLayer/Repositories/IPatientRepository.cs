using MyDocAppointment.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public interface IPatientRepository
    {
        void Add(Patient patient);
        void Delete(int id);
        IEnumerable<Patient> GetAll();
        Patient? GetById(int id);
        void Update(Patient patient);
    }
}
