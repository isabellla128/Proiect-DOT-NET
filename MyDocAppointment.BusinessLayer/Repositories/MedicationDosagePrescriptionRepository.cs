﻿using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class MedicationDosagePrescriptionRepository : Repository<MedicationDosagePrescription>
    {
        public MedicationDosagePrescriptionRepository(TestsDatabaseContext context) : base(context)
        {
        }
    }
}
