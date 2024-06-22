using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_financial_system.Models.Domain;
[Table("Version")]
public class Version
{
    [Key]
    public int IdVersion { get; set; }
    [MaxLength(200)]
    [Required]
    public string Information { get; set; }
    [Required]
    [MaxLength(10)]
    public string Level { get; set; }
    [Required]
    public int IdSoftware { get; set; }
    
    [ForeignKey(nameof(IdSoftware))]
    public Software Software { get; set; }
}