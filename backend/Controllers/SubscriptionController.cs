using AutoMapper;
using EloquentBackend.DTOs;
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
        private readonly IMapper _mapper; // Injetar o IMapper

        public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper; // Atribuir a instância
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
            var subscriptionsToReturn = _mapper.Map<List<SubscriptionDto>>(subscriptions);

            return Ok(subscriptionsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscriptionById(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
            if (subscription == null)
                return NotFound();

            // Mapeie o objeto encontrado para o DTO de retorno
            var subscriptionToReturn = _mapper.Map<SubscriptionDto>(subscription);

            return Ok(subscriptionToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionDto data)
        {
            if (data == null)
                return BadRequest();

            var subscription = _mapper.Map<Subscription>(data);
            var createdSubscription = await _subscriptionService.CreateSubscriptionAsync(
                subscription
            );

            var subscriptionToReturn = _mapper.Map<SubscriptionDto>(createdSubscription);

            return CreatedAtAction(
                nameof(GetSubscriptionById),
                new { id = subscriptionToReturn.Id },
                subscriptionToReturn
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubscription(int id, Subscription subscription)
        {
            if (id != subscription.Id)
                return BadRequest();

            var updatedSubscription = await _subscriptionService.UpdateSubscriptionAsync(
                subscription
            );
            if (updatedSubscription == null)
                return NotFound();
            return Ok(updatedSubscription);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var deleted = await _subscriptionService.DeleteSubscriptionAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
