using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_financial_system.Models.Domain;
[Table("Software")]
public class Software
{
    [Key]
    public int IdSoftware { get; set; }
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
    [MaxLength(100)]
    [Required]
    public string Description { get; set; }
    [Required]
    public int IdCategory { get; set; }
    [Required]
    public decimal PriceForYear { get; set; }
    
    [ForeignKey(nameof(IdCategory))]
    public Category Category { get; set; }

    public ICollection<Version> Versions { get; set; }
}