using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_financial_system.Models.Domain;
[Table("Contract")]
public class Contract
{
    [Key]
    public int IdContract { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    [Range(3, 30, ErrorMessage = "The value must be between 3 and 30.")]
    public int DayInterval { get; set; }
    [Required]
    public int IdDiscount { get; set; }
    [Required]
    [MaxLength(100)]
    public string UpdatesInfo { get; set; }
    [Required]
    public bool IsSigned { get; set; }
    [Required]
    public decimal AmountPaid { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    [Range(1, 4, ErrorMessage = "The value must be between 1 and 4.")]
    public int YearsOfSupport { get; set; }
    [Required]
    public int IdVersion { get; set; }
    [Required]
    public int IdCustomer { get; set; }
    
    [ForeignKey(nameof(IdDiscount))]
    public Discount Discount { get; set; }
    
    [ForeignKey(nameof(IdVersion))]
    public Version Version { get; set; }
    
    [ForeignKey(nameof(IdCustomer))]
    public Customer Customer { get; set; }
}