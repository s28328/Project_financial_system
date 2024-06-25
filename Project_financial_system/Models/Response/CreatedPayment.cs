namespace Project_financial_system.Models.Response;

public class CreatedPayment
{
    public int IdPayment { get; set; }
    public DateTime Date { get; set; }
    public int IdContract { get; set; }
    public decimal Quota { get; set; }
    public int IdCustomer { get; set; }
}