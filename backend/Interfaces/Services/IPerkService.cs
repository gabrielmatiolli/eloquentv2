using EloquentBackend.Models;

namespace EloquentBackend.Interfaces.Services
{
  public interface IPerkService
  {
    public Task<IEnumerable<Perk>> GetAllPerksAsync();

    public Task<Perk?> GetPerkByIdAsync(int id);

    public Task<Perk> CreatePerkAsync(Perk perk);

    public Task<Perk> UpdatePerkAsync(Perk perk);

    public Task<bool> DeletePerkAsync(int id);

  }
}