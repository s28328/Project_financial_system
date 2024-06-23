namespace Project_financial_system.Models.Request;

public class RequestContract
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DayInterval { get; set; } 
    public string UpdatesInfo { get; set; }
    public int YearsOfSupport { get; set; }
    public int IdVersion { get; set; }
    public int IdCustomer { get; set; }
    
}