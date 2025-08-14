using EloquentBackend.Models;

namespace EloquentBackend.Interfaces.Services
{
  public interface ISubscriptionService
  {
    Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
    Task<Subscription> GetSubscriptionByIdAsync(int id);
    Task<Subscription> CreateSubscriptionAsync(Subscription subscription);
    Task<Subscription> UpdateSubscriptionAsync(Subscription subscription);
    Task<bool> DeleteSubscriptionAsync(int id);
  }
}