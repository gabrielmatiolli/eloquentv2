using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required] // O nome de uma categoria não deve ser nulo
        [MaxLength(50)] // Define um tamanho máximo para o campo no banco de dados
        public required string Name { get; set; }

        // Propriedade de navegação para EF Core saber que uma Categoria pode ter vários Registros
        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}