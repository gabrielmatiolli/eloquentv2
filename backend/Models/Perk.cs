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

        // Propriedade de navegação para o relacionamento muitos-para-muitos com Subscription
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}