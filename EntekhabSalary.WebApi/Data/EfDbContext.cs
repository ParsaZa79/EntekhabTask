using EntekhabSalary.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntekhabSalary.WebApi.Data;

public class EfDbContext : DbContext
{
    public EfDbContext(DbContextOptions<EfDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 2);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>().Property(e => e.Id)
            .UseIdentityColumn()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
        
        modelBuilder.Entity<EmployeeSalary>()
            .HasOne(es => es.Employee)
            .WithMany()
            .HasForeignKey(es => es.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}