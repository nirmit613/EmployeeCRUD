using EmployeeCrud.Models.Interfaces;
using EmployeeCrud.Models.Models;
using EmployeeCrud.Models.Repository;
using EmployeeCrud.Services.AutoMapperProfile;
using EmployeeCrud.Services.Interfaces;
using EmployeeCrud.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors File
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsapp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// dbContext Setup

builder.Services.AddDbContext<AppDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeConnString"), x => x.MigrationsAssembly("EmployeeCrud.Models"));
    Options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

#region Dependency Injection
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
#endregion

// Automapper Field 

builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
