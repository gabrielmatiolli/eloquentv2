using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.DTOs
{
    public record SubscriptionDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required decimal Price { get; init; }
        public required List<SubscriptionPerkDto> SubscriptionPerks { get; init; }
    }

    public record CreateSubscriptionDto
    {
        [Required(ErrorMessage = "O nome da assinatura é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public required string name { get; init; }
        public string? description { get; init; }
        public decimal price { get; init; }
        public List<CreateSubscriptionPerkDto> subscriptionPerks { get; init; } =
            new List<CreateSubscriptionPerkDto>();
    }
}
