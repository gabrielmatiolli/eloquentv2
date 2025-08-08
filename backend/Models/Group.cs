namespace EloquentBackend.Models
{
  public class Group
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Subscription Subscription { get; set; }
    public User UserAdmin { get; set; }
    public User[] Users { get; set; }
    public string ImageUrl { get; set; }
    public string Color { get; set; }
  }
}
