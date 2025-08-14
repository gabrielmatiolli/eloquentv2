using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.DTOs
{
    public record CreatePerkDto
    {
        [Required(ErrorMessage = "O nome do perk é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public required string perkName { get; init; }
        public string? perkDescription { get; init; }
    }
}