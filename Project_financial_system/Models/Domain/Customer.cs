using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_financial_system.Models.Domain;
[Table("Customer")]
public abstract class Customer
{
    [Key]
    public int IdCustomer { get; set; }
    [MaxLength(30)]
    [Required]
    public string Email { get; set; }
    [MaxLength(15)]
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public int IdAddress { get; set; }
    
    [ForeignKey(nameof(IdAddress))]
    public Address Address { get; set; }

    public ICollection<Contract> Contracts { get; set; }
    public ICollection<Payment> Payments { get; set; }
}