using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_financial_system.Models.Domain;
[Table("AppUser")]
public class AppUser
{
    [Key]
    public int IdUser { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
}