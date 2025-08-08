using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.Models
{
  public class Perk
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
}
