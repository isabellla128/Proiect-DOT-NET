using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MyDocAppointment.BusinessLayer.Data;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class BaseIntegrationTests<T> : IClassFixture<CustomWebApplicationFactory<Program>> where T : class
    {
        protected readonly CustomWebApplicationFactory<Program> factory;
        protected readonly HttpClient httpClient;

        protected BaseIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            this.factory = factory;
            this.httpClient = factory.CreateClient();

            CleanDatabases();
        }

        private void CleanDatabases()
        {
            using (var scope = factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var databaseContext = scopedServices.GetRequiredService<TestsDatabaseContext>();

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
}
