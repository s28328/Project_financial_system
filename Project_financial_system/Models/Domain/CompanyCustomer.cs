using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_financial_system.Models.Domain;

[Table("Company_Customer")]
public class CompanyCustomer:Customer
{
        [MaxLength(100)]
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string KRS { get; set; }
}