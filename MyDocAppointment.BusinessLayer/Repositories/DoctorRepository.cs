﻿using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class DoctorRepository : Repository<Doctor>
    {
        public DoctorRepository(TestsDatabaseContext context) : base(context)
        {
        }

    }
}
