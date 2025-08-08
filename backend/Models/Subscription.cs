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
    [Column(TypeName = "decimal(18,2)")] // Define o tipo exato no banco de dados
    public decimal Price { get; set; }

    // Relação muitos-para-muitos com Perk
    public ICollection<Perk> Perks { get; set; } = new List<Perk>();
  }
}