using System.ComponentModel.DataAnnotations.Schema;

namespace EloquentBackend.Models
{
    public class SubscriptionPerk
    {
        // Chaves estrangeiras que formarão a chave primária composta
        public int SubscriptionId { get; set; }
        public int PerkId { get; set; }

        // Propriedades de navegação para as entidades principais
        public Subscription Subscription { get; set; } = null!;
        public Perk Perk { get; set; } = null!;

        // --- Payload: O valor específico desta relação ---
        // Armazena o valor se o perk for numérico (ex: "Limite de 500 itens")
        [Column(TypeName = "decimal(18,2)")]
        public decimal? NumericValue { get; set; }

        // Armazena o valor se o perk for booleano (ex: "Acesso a Suporte VIP?")
        public bool? BooleanValue { get; set; }
    }
}
