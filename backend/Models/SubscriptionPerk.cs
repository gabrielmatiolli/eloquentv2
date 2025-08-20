using System.ComponentModel.DataAnnotations.Schema;

namespace EloquentBackend.Models
{
    public class SubscriptionPerk
    {
        public int SubscriptionId { get; set; }
        public int PerkId { get; set; }
        public Subscription Subscription { get; set; } = null!;
        public Perk Perk { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Value { get; set; }
    }
}
