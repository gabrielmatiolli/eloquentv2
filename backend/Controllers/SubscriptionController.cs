using EloquentBackend.Interfaces.Services;
using EloquentBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EloquentBackend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SubscriptionController : ControllerBase
  {
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
      _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubscriptions()
    {
      var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
      return Ok(subscriptions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubscriptionById(int id)
    {
      var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
      if (subscription == null) return NotFound();
      return Ok(subscription);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(Subscription subscription)
    {
      if (subscription == null) return BadRequest();
      var createdSubscription = await _subscriptionService.CreateSubscriptionAsync(subscription);
      return CreatedAtAction(nameof(GetSubscriptionById), new { id = createdSubscription.Id }, createdSubscription);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubscription(int id, Subscription subscription)
    {
      if (id != subscription.Id) return BadRequest();

      var updatedSubscription = await _subscriptionService.UpdateSubscriptionAsync(subscription);
      if (updatedSubscription == null) return NotFound();
      return Ok(updatedSubscription);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscription(int id)
    {
      var deleted = await _subscriptionService.DeleteSubscriptionAsync(id);
      if (!deleted) return NotFound();
      return NoContent();
    }
  }
}