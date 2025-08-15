using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.DTOs
{
    public record CreateSubscriptionPerkDto
    {
        [Required(ErrorMessage = "O ID do perk é obrigatório.")]
        public required int perkId { get; init; }
        public bool? booleanValue { get; init; }
        public int? numericValue { get; init; }
    }

    public record SubscriptionPerkDto
    {
        public int Id { get; init; }
        public int PerkId { get; init; }
        public bool? BooleanValue { get; init; }
        public int? NumericValue { get; init; }
        public required PerkDto Perk { get; init; } // <-- Mudança: agora é um único objeto
    }
}
