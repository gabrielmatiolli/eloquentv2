using System.ComponentModel.DataAnnotations;

namespace EloquentBackend.Models
{
  public class Subscription
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public Perk[] Perks { get; set; }
  }
}
