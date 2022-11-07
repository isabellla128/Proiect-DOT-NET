using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Appointment
    {
        public Appointment(Patient patient, Doctor doctor, DateTime startTime, DateTime endTime)
        {
            Doctor = doctor;
            Patient = patient;
            StartTime = startTime;
            EndTime = endTime;
        }

        public int Id { get; private set; }
        public Doctor Doctor { get; private set; }
        public Patient Patient { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

    }
}
