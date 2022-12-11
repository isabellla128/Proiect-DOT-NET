using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MyDocAppointment.API;
using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;
using System.Reflection;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                                                 .AllowAnyHeader()
                                                 .AllowAnyMethod();
                      });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

//builder.Services.AddMvc();

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson(x => { x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });

builder.Services.AddDbContext<MyDocAppointmentDatabaseContext>(options => options.UseSqlite("Data Source = dbMyDocAppointmentManagement.db"));

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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }