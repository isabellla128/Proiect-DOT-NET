using Microsoft.AspNetCore.Mvc.Testing;
using MyDocAppointment.BusinessLayer.Data;

namespace MyDocAppointment.API.Tests
{
    public class BaseIntegrationTests <T> where T : class
    {
        protected HttpClient  HttpClient { get;private set; }
        protected BaseIntegrationTests()
        {
            var application = new WebApplicationFactory<T>().WithWebHostBuilder(builder => { });
            HttpClient = application.CreateClient();
            CleanDatabases();
        }

        private void CleanDatabases()
        {
            var databaseContext = new MyDocAppointmentDatabaseContext();
            databaseContext.Hospitals.RemoveRange(databaseContext.Hospitals.ToList());
            databaseContext.Patients.RemoveRange(databaseContext.Patients.ToList());
            databaseContext.Appointments.RemoveRange(databaseContext.Appointments.ToList());
            databaseContext.Doctors.RemoveRange(databaseContext.Doctors.ToList());
            databaseContext.Medications.RemoveRange(databaseContext.Medications.ToList());
            databaseContext.Prescriptions.RemoveRange(databaseContext.Prescriptions.ToList());
            databaseContext.MedicationDosagePrescriptions.RemoveRange(databaseContext.MedicationDosagePrescriptions.ToList());
            //databaseContext.Schedules.RemoveRange(databaseContext.Schedules.ToList());
            databaseContext.SaveChanges();
        }
    }
}
