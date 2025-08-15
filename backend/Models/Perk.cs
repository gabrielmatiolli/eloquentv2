using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.Models
{
    public class Perk
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        public string? Description { get; set; }

        // A coleção agora é da entidade de associação
        public ICollection<SubscriptionPerk> SubscriptionPerks { get; set; } = new List<SubscriptionPerk>();
    }
}