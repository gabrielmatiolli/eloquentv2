using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EloquentBackend.Enums;

namespace EloquentBackend.Models
{
  public class Record
  {
    [Key]
    public int Id { get; set; }

    public DateTime Date { get; set; }
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    [Required]
    public RecordType Type { get; set; }

    // --- Chaves Estrangeiras e Navegação ---

    // Relação com User
    public int UserId { get; set; } // Chave estrangeira explícita
    public required User User { get; set; }   // Propriedade de navegação

    // Relação com Category
    public int CategoryId { get; set; } // Chave estrangeira explícita
    public Category? Category { get; set; } // Propriedade de navegação
  }
}