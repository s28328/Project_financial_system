namespace Project_financial_system.Models.Response;

public class CreatedIndividualCustomerResponse
{
    public int IdCustomer { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int IdAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PESEL { get; set; }
}