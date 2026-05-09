using Microsoft.AspNetCore.Mvc;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api")]
public class StandingsController : ControllerBase
{
    private readonly IStandingsService _standingsService;

    public StandingsController(IStandingsService standingsService)
    {
        _standingsService = standingsService;
    }

    // Tabla de posiciones
    [HttpGet("standings")]
    public async Task<IActionResult> GetStandings([FromQuery] int tournamentId)
    {
        var standings = await _standingsService.GetStandingsAsync(tournamentId);
        return Ok(standings);
    }

    // Goleadores
    [HttpGet("stats/scorers")]
    public async Task<IActionResult> GetTopScorers([FromQuery] int tournamentId)
    {
        var scorers = await _standingsService.GetTopScorersAsync(tournamentId);
        return Ok(scorers);
    }

    // Tarjetas
    [HttpGet("stats/cards")]
    public async Task<IActionResult> GetCardStats([FromQuery] int tournamentId)
    {
        var cards = await _standingsService.GetCardStatsAsync(tournamentId);
        return Ok(cards);
    }
}
