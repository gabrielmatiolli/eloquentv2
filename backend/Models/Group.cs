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
    public int SubscriptionId { get; set; }
    public required Subscription Subscription { get; set; }
    public int UserAdminId { get; set; }
    public required User UserAdmin { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
  }
}