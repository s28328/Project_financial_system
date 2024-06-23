namespace Project_financial_system.Models.DTO;

public class CompanyCustomerDto
{
    public string Email { get; set; }
    public AddressDto Address { get; set; }
    public string PhoneNumber { get; set; }
    public string CompanyName { get; set; }
    public string KRS { get; set; }
}