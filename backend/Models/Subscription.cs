using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EloquentBackend.Models
{
  public class Subscription
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int? PerkValue { get; set; }
    public ICollection<Perk> Perks { get; set; } = new List<Perk>();
  }
}