using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(50)]
        public required string Name { get; set; }

        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}