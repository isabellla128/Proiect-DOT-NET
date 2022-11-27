using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson(x => { x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });

builder.Services.AddScoped<MyDocAppointmentDatabaseContext>();

builder.Services.AddScoped<IRepository<Doctor>, DoctorRepository>();
builder.Services.AddScoped<IRepository<Hospital>, HospitalRepository>();
builder.Services.AddScoped<IRepository<Patient>, PatientRepository>();
builder.Services.AddScoped<IRepository<History>, HistoryRepository>();
builder.Services.AddScoped<IRepository<Medication>, MedicationRepositrory>();
builder.Services.AddScoped<IRepository<Prescription>, PrescriptionRepository>();
builder.Services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
builder.Services.AddScoped<IRepository<Event>, EventRepositrory>();
builder.Services.AddScoped<IRepository<Schedule>, ScheduleRepository>();
builder.Services.AddScoped<IRepository<MedicationDosageHistory>, MedicationDosageHistoryRepository>();
builder.Services.AddScoped<IRepository<MedicationDosagePrescription>, MedicationDosagePrescriptionRepository>();



var config = new ConfigurationBuilder();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
