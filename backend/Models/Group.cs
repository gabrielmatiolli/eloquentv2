using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.Models
{
  public class Group
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? ImageUrl { get; set; }
    public string? Color { get; set; }

    // --- Chaves Estrangeiras e Navegação ---

    // Relação com Subscription
    public int SubscriptionId { get; set; }
    public required Subscription Subscription { get; set; }

    // Relação com o User que é admin
    public int UserAdminId { get; set; }
    public required User UserAdmin { get; set; }

    // Relação muitos-para-muitos com os membros do grupo
    public ICollection<User> Users { get; set; } = new List<User>();
  }
}