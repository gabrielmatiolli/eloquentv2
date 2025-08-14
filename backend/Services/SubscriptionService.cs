using EloquentBackend.Data;
using EloquentBackend.Interfaces.Services;
using EloquentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EloquentBackend.Services
{
  public class SubscriptionService : ISubscriptionService
  {
    private readonly ApiDbContext _db;

    public SubscriptionService(ApiDbContext db)
    {
      _db = db;
    }

    public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync()
    {
      return await _db.Subscriptions.ToListAsync();
    }

    public async Task<Subscription> GetSubscriptionByIdAsync(int id)
    {
      return await _db.Subscriptions.FindAsync(id) ?? throw new KeyNotFoundException("Subscription not found");
    }

    public async Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
    {
      if (subscription == null) throw new ArgumentNullException(nameof(subscription));
      _db.Subscriptions.Add(subscription);
      await _db.SaveChangesAsync();
      return subscription;
    }

    public async Task<Subscription> UpdateSubscriptionAsync(Subscription subscription)
    {
      if (subscription == null) throw new ArgumentNullException(nameof(subscription));
      _db.Subscriptions.Update(subscription);
      await _db.SaveChangesAsync();
      return subscription;
    }

    public async Task<bool> DeleteSubscriptionAsync(int id)
    {
      var subscription = await GetSubscriptionByIdAsync(id);
      if (subscription == null) return false;
      _db.Subscriptions.Remove(subscription);
      await _db.SaveChangesAsync();
      return true;
    }
  }
}