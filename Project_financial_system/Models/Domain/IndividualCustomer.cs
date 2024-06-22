using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_financial_system.Models.Domain;
[Table("Individual_Customer")]
public class IndividualCustomer:Customer
{
    [MaxLength(30)]
    [Required]
    public string FirstName { get; set; }
    [MaxLength(30)]
    [Required]
    public string LastName { get; set; }
    [MaxLength(11)]
    [Required]
    public string PESEL { get; set; }
    [Required]
    public bool IsDeleted { get; set; } = false; // Soft delete
}