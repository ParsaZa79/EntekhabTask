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

ConfigureServices(builder.Services, builder.Configuration);
ConfigureSwagger(builder.Services);

MappingConfig.RegisterMappings();

var app = builder.Build();

ConfigureApp(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();

    services.AddScoped<IEmployeeService, EmployeeService>();
    services.AddScoped<IEmployeeSalaryService, EmployeeSalaryService>();

    services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    services.AddScoped<IEmployeeSalaryRepository, EmployeeSalaryRepository>();

    services.AddSingleton<IDataSerializerFactory, DataSerializerFactory>();
    services.AddSingleton<IOvertimePolicyCalculatorFactory, OvertimePolicyCalculatorFactory>();

    services.AddScoped<ISalaryCalculator, DefaultSalaryCalculator>();

    services.AddSingleton<DapperDbContext>();

    var connectionString = configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<EfDbContext>(options => options.UseSqlite(connectionString));

    services.AddLogging();
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
}

void ConfigureApp(WebApplication webApplication)
{
    if (webApplication.Environment.IsDevelopment())
    {
        webApplication.UseSwagger();
        webApplication.UseSwaggerUI();
    }

    webApplication.UseHttpsRedirection();

    webApplication.UseAuthorization();

    webApplication.MapControllers();
}
