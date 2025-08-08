using System.ComponentModel.DataAnnotations;
using EloquentBackend.Enums;

namespace EloquentBackend.Models
{
  public class Record
  {
    [Key]
    public int Id { get; set; }
    public User User { get; set; }
    public RecordType Type { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public int Value { get; set; }
    public Category Category { get; set; }
  }
}