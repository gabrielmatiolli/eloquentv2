using EloquentBackend.Data;
using EloquentBackend.Interfaces.Services;
using EloquentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EloquentBackend.Services
{
    public class SubscriptionPerkService : ISubscriptionPerkService
    {
        private readonly ApiDbContext _db;

        public SubscriptionPerkService(ApiDbContext db)
        {
            _db = db;
        }

        public async Task<List<SubscriptionPerk>> GetPerksAsync()
        {
            return await _db
                .SubscriptionPerks.Include(p => p.Subscription)
                .Include(p => p.Perk)
                .ToListAsync();
        }

        public async Task<List<SubscriptionPerk>> GetSubscriptionPerksBySubscriptionAsync(
            int subscriptionId
        )
        {
            return await _db
                .SubscriptionPerks.Where(p => p.SubscriptionId == subscriptionId)
                .Include(p => p.Subscription)
                .Include(p => p.Perk)
                .ToListAsync();
        }

        public async Task<List<SubscriptionPerk>> UpdateSubscriptionPerksAsync(
            int subscriptionId,
            List<SubscriptionPerk> subscriptionPerks
        )
        {
            var existingPerks = await GetSubscriptionPerksBySubscriptionAsync(subscriptionId);

            _db.SubscriptionPerks.RemoveRange(existingPerks);
            await _db.SubscriptionPerks.AddRangeAsync(subscriptionPerks);
            await _db.SaveChangesAsync();

            return subscriptionPerks;
        }
    }
}
