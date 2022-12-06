using Microsoft.EntityFrameworkCore.Migrations;
using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using MyDocDoctor.BusinessLayer.Repositories;
using MyDocEvent.BusinessLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson(x => { x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });

builder.Services.AddScoped<IDatabaseContext, MyDocAppointmentDatabaseContext>();

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IHistoryRepository1, HistoryRepository1>();
builder.Services.AddScoped<IMedicationRepositrory, MedicationRepositrory>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IEventRepositrory, EventRepositrory>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IMedicationDosageHistoryRepository, MedicationDosageHistoryRepository>();
builder.Services.AddScoped<IMedicationDosagePrescriptionRepository, MedicationDosagePrescriptionRepository>();

// Create open SqliteConnection so EF won't automatically close it.
//builder.Services.AddSingleton<DbConnection>(container =>
//{
//    var connection = new SqliteConnection("DataSource=:memory:");
//    connection.Open();

//    return connection;
//});

//builder.Services.AddDbContext<TestsDatabaseContext>((container, options) =>
//{
//    var connection = container.GetRequiredService<DbConnection>();
//    options.UseSqlite(connection);
//});


//var conn = new SqliteConnection("Filename=:memory:");
//conn.Open();
//builder.Services.AddDbContext<TestsDatabaseContext>(c => c.UseSqlite(conn));


var config = new ConfigurationBuilder();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
