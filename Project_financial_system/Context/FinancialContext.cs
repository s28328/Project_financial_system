using Microsoft.EntityFrameworkCore;
using Project_financial_system.Models;
using Project_financial_system.Models.Domain;

namespace Project_financial_system.Context;

public class FinancialContext:DbContext
{
    protected FinancialContext()
    {
    }

    public FinancialContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CompanyCustomer> CompanyCustomers { get; set; }
    public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .ToTable("Customer");
        modelBuilder.Entity<CompanyCustomer>()
            .ToTable("Company_Customer");
        modelBuilder.Entity<IndividualCustomer>()
            .ToTable("Individual_Customer");
    }
}