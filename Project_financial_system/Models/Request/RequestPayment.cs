using System.ComponentModel.DataAnnotations;

namespace Project_financial_system.Models.Request;

public class RequestPayment
{
    public int IdCustomer { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public decimal Quota { get; set; }
}