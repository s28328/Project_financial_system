using Microsoft.EntityFrameworkCore;

namespace Project_financial_system.Context;

public class FinancialContext:DbContext
{
    protected FinancialContext()
    {
    }

    public FinancialContext(DbContextOptions options) : base(options)
    {
    }
}