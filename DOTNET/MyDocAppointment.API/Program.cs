using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyDocAppointment.API;
using MyDocAppointment.BusinessLayer.Data;

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

builder.Services.AddScoped<TenantService>();

// Add services to the container.
builder.Services.AddScoped<MultiTenantServiceMiddleware>();
builder.Services.AddDbContext<MyDocAppointmentDatabaseContext>(db => {
    db.UseSqlite("Data Source=multi-tenant.db");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson(x => { x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

//initialize the database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyDocAppointmentDatabaseContext>();
    await db.Database.MigrateAsync();
}

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

app.UseMiddleware<MultiTenantServiceMiddleware>();
// multi-tenant request, try adding ?tenant=Khalid or ?tenant=Internet (default)
app.MapGet("/", async (MyDocAppointmentDatabaseContext db) => await db
    .Doctors
    // hide the tenant, which is response noise
    .Select(x => new { x.Id, x.FullName })
    .ToListAsync());

app.Run();

public partial class Program
{
    protected Program()
    {
    }
}
