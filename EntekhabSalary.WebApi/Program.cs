using System.Reflection;
using EntekhabSalary.WebApi.Data;
using EntekhabSalary.WebApi.Helper.Config;
using EntekhabSalary.WebApi.Helper.Deserializer.Factory;
using EntekhabSalary.WebApi.Helper.OvertimePolicy;
using EntekhabSalary.WebApi.Helper.SalaryCalculator;
using EntekhabSalary.WebApi.Repositories.Abstract;
using EntekhabSalary.WebApi.Repositories.Concrete;
using EntekhabSalary.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeSalaryService, EmployeeSalaryService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeSalaryRepository, EmployeeSalaryRepository>();

builder.Services.AddSingleton<IDataSerializerFactory, DataSerializerFactory>();
builder.Services.AddSingleton<IOvertimePolicyCalculatorFactory, OvertimePolicyCalculatorFactory>();

builder.Services.AddScoped<ISalaryCalculator, DefaultSalaryCalculator>();

builder.Services.AddSingleton<DapperDbContext>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EfDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddLogging();

MappingConfig.RegisterMappings();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
