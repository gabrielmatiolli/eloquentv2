using AutoMapper;
using EloquentBackend.DTOs;
using EloquentBackend.Interfaces.Services;
using EloquentBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EloquentBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPerkController : ControllerBase
    {
        private readonly ISubscriptionPerkService _service;

        private readonly IMapper _mapper;

        public SubscriptionPerkController(ISubscriptionPerkService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPerks()
        {
            var perks = await _service.GetPerksAsync();
            var perkDtos = _mapper.Map<List<SubscriptionPerkDto>>(perks);
            return Ok(perkDtos);
        }

        [HttpGet("subscription/{subscriptionId}")]
        public async Task<IActionResult> GetPerksBySubscription(int subscriptionId)
        {
            var perks = await _service.GetSubscriptionPerksBySubscriptionAsync(subscriptionId);
            var perkDtos = _mapper.Map<List<SubscriptionPerkDto>>(perks);
            return Ok(perkDtos);
        }

        [HttpPut("subscription/{subscriptionId}")]
        public async Task<IActionResult> UpdatePerks(
            int subscriptionId,
            List<CreateSubscriptionPerkDto> perkDtos // Correct DTO for the input
        )
        {
            var perks = _mapper.Map<List<SubscriptionPerk>>(perkDtos);
            foreach (var perk in perks)
            {
                perk.SubscriptionId = subscriptionId;
            }
            var updatedPerks = await _service.UpdateSubscriptionPerksAsync(subscriptionId, perks);
            var updatedPerkDtos = _mapper.Map<List<SubscriptionPerkDto>>(updatedPerks);
            return Ok(updatedPerkDtos);
        }
    }
}
