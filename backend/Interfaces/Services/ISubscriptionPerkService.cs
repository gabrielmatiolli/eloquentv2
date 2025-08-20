using EloquentBackend.Models;

namespace EloquentBackend.Interfaces.Services
{
    public interface ISubscriptionPerkService
    {
        Task<List<SubscriptionPerk>> GetPerksAsync();
        Task<List<SubscriptionPerk>> GetSubscriptionPerksBySubscriptionAsync(int subscriptionId);

        Task<List<SubscriptionPerk>> UpdateSubscriptionPerksAsync(
            int subscriptionId,
            List<SubscriptionPerk> subscriptionPerks
        );
    }
}
