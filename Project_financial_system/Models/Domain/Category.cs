using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_financial_system.Models.Domain;
[Table("Category")]
public class Category
{
    [Key]
    public int IdCategory { get; set; }
    [MaxLength(30)]
    [Required]
    public string Name { get; set; }

    public ICollection<Software> Softwares { get; set; }
}