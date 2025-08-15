using EloquentBackend.Data;
using EloquentBackend.Interfaces.Services;
using EloquentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EloquentBackend.Services
{
    public class PerkService : IPerkService
    {
        private readonly ApiDbContext _db;

        public PerkService(ApiDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Perk>> GetAllPerksAsync()
        {
            return await _db
                .Perks.Include(p => p.SubscriptionPerks)
                .ThenInclude(sp => sp.Subscription)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Perk?> GetPerkByIdAsync(int id)
        {
            return await _db
                .Perks.Include(p => p.SubscriptionPerks)
                .ThenInclude(sp => sp.Subscription)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Perk> CreatePerkAsync(Perk perk)
        {
            if (perk == null)
                throw new ArgumentNullException(nameof(perk));
            _db.Perks.Add(perk);
            await _db.SaveChangesAsync();
            return perk;
        }

        public async Task<Perk> UpdatePerkAsync(Perk perk)
        {
            if (perk == null)
                throw new ArgumentNullException(nameof(perk));
            _db.Perks.Update(perk);
            await _db.SaveChangesAsync();
            return perk;
        }

        public async Task<bool> DeletePerkAsync(int id)
        {
            var perk = await _db.Perks.FindAsync(id);
            if (perk == null)
                return false;

            _db.Perks.Remove(perk);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
