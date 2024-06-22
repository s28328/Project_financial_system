using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_financial_system.Models.Domain;
[Table("Payment")]
public class Payment
{
    [Key]
    public int IdPayment { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int IdContract { get; set; }
    [Required]
    public decimal Quota { get; set; }
    [Required]
    public int IdCustomer { get; set; }
    
    [ForeignKey(nameof(IdContract))]
    public Contract Contract { get; set; }
    
    [ForeignKey(nameof(IdCustomer))]
    public Customer Customer { get; set; }
}