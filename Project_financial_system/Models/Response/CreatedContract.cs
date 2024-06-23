namespace Project_financial_system.Models.Response;

public class CreatedContract
{
    public int IdContract { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DayInterval { get; set; } 
    public string UpdatesInfo { get; set; }
    public int YearsOfSupport { get; set; }
    public decimal FinalPrice { get; set; }
    public decimal AmountPaid { get; set; }
    public int? IdDiscount { get; set; }
    public int IdVersion { get; set; }
    public int IdCustomer { get; set; }
    
}