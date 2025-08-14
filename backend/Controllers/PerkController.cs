using EloquentBackend.DTOs;
using EloquentBackend.Interfaces.Services;
using EloquentBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EloquentBackend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PerkController : ControllerBase
  {
    private readonly IPerkService _perkService;

    public PerkController(IPerkService perkService)
    {
      _perkService = perkService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPerks()
    {
      var perks = await _perkService.GetAllPerksAsync();
      return Ok(perks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerk(int id)
    {
      var perk = await _perkService.GetPerkByIdAsync(id);
      if (perk == null) return NotFound();
      return Ok(perk);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerk([FromBody] Perk perk)
    {
      if (perk == null) return BadRequest();
      var createdPerk = await _perkService.CreatePerkAsync(perk);
      return CreatedAtAction(nameof(GetPerk), new { id = createdPerk.Id }, createdPerk);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerk([FromBody] Perk perk)
    {
      var updatedPerk = await _perkService.UpdatePerkAsync(perk);
      if (updatedPerk == null) return NotFound();

      return Ok(updatedPerk);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerk(int id)
    {
      var deleted = await _perkService.DeletePerkAsync(id);
      if (!deleted) return NotFound();

      return NoContent();
    }
  }
}