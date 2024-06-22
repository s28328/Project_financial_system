using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_financial_system.Models.Domain;

namespace Project_financial_system.Models;
[Table("Address")]
public class Address
{
    [Key]
    public int IdAddress { get; set; }
    [MaxLength(100)]
    [Required]
    public string Street { get; set; }
    [MaxLength(100)]
    [Required]
    public string City { get; set; }
    [MaxLength(10)]
    [Required]
    public string PostalCode { get; set; }

    public ICollection<Customer> Customers { get; set; }
}