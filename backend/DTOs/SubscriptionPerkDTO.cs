using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.DTOs
{
    public record CreateSubscriptionPerkDto
    {
        [Required(ErrorMessage = "O ID do perk é obrigatório.")]
        public required int perkId { get; init; }

        [Required(ErrorMessage = "O ID do subscription é obrigatório.")]
        public required int subscriptionId { get; init; }
        public int? value { get; init; }
    }

    public record SubscriptionPerkDto
    {
        public int Id { get; init; }
        public int PerkId { get; init; }
        public int SubscriptionId { get; init; }
        public int? Value { get; init; }
        public required PerkDto Perk { get; init; }
    }
}
