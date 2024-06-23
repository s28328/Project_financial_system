using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Project_financial_system.Context;

public class TransactionMiddleware
{
    private readonly RequestDelegate _next;

    public TransactionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, FinancialContext dbContext)
    {
        // Begin the transaction
        using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            // Call the next middleware in the pipeline
            await _next(context);

            // Commit the transaction if no exceptions occurred
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            // Roll back the transaction if an exception occurred
            await transaction.RollbackAsync();
            throw; // Re-throw the exception after rolling back the transaction
        }
    }
}