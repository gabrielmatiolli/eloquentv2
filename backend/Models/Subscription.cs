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

        // A coleção agora é da entidade de associação
        public ICollection<SubscriptionPerk> SubscriptionPerks { get; set; } =
            new List<SubscriptionPerk>();
    }
}
