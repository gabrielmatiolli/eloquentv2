using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Email { get; set; } // Considere adicionar um índice de unicidade aqui no DbContext

        [Required]
        public required string Password { get; set; } // Lembre-se de sempre armazenar o hash, nunca a senha pura

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Valor padrão

        public bool IsAdmin { get; set; }

        // Propriedades de endereço e contato (opcionais, então podem ser nulas)
        public string? ProfileImageUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? NumberStreet { get; set; }

        // Relacionamentos
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}