namespace Project_financial_system.Models.Response;

public class CreatedCompanyCustomerResponse
{
    public int IdCustomer { get; set; }
    public string Email { get; set; }
    public int IdAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string CompanyName { get; set; }
    public string KRS { get; set; }
}