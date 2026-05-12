using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [ApiController]
    [Route("api/match/{matchId}/lineup")]
    public class MatchLineupController : ControllerBase
    {
        private readonly IMatchLineupService _lineupService;
        private readonly IMapper _mapper;

        public MatchLineupController(IMatchLineupService lineupService, IMapper mapper)
        {
            _lineupService = lineupService;
            _mapper = mapper;
        }

        // ── POST: Agregar jugador a la alineación ──
        [HttpPost]
        public async Task<ActionResult<MatchLineupResponseDTO>> AddPlayer(
            int matchId,
            [FromBody] MatchLineupRequestDTO request)
        {
            var lineup = await _lineupService.AddPlayerToLineupAsync(
                matchId,
                request.PlayerId,
                request.IsStarter,
                request.Position);

            var response = _mapper.Map<MatchLineupResponseDTO>(lineup);
            return CreatedAtAction(nameof(GetLineupByMatch), new { matchId }, response);
        }

        // ── GET: Obtener alineación completa ──
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchLineupResponseDTO>>> GetLineupByMatch(int matchId)
        {
            var lineups = await _lineupService.GetLineupByMatchAsync(matchId);
            var response = _mapper.Map<IEnumerable<MatchLineupResponseDTO>>(lineups);
            return Ok(response);
        }

        // ── GET: Obtener alineación por equipo ──
        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<MatchLineupResponseDTO>>> GetLineupByTeam(int matchId, int teamId)
        {
            var lineups = await _lineupService.GetLineupByMatchAndTeamAsync(matchId, teamId);
            var response = _mapper.Map<IEnumerable<MatchLineupResponseDTO>>(lineups);
            return Ok(response);
        }

        // ── DELETE: Eliminar jugador de la alineación ──
        [HttpDelete("{lineupId}")]
        public async Task<IActionResult> DeleteLineup(int matchId, int lineupId)
        {
            await _lineupService.DeleteLineupAsync(matchId, lineupId);
            return NoContent();
        }
    }
}
