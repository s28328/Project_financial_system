using Microsoft.EntityFrameworkCore;
using Project_financial_system.Context;
using Project_financial_system.Repositories;
using Project_financial_system.Repositories.Abstraction;
using Project_financial_system.Services;
using Project_financial_system.Services.Abstraction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICompanyCustomerRepository,CompanyCustomerRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IIndividualCustomerRepository, IndividualCustomerRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IDiscountRepository,DiscountRepository>();
builder.Services.AddScoped<IVersionRepository,VersionRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRevenueService, RevenueService>();
builder.Services.AddDbContext<FinancialContext>(
    options => options.UseNpgsql("Host=localhost; Database=financial_system; Username=postgres; Password=admin; Pooling = true;"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<TransactionMiddleware>();
app.MapControllers();

app.Run();
