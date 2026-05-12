using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Application.Services
{
    public class MatchLineupService : IMatchLineupService
    {
        private readonly IMatchLineupRepository _repository;

        public MatchLineupService(IMatchLineupRepository repository)
        {
            _repository = repository;
        }

        public async Task<MatchLineup> AddPlayerToLineupAsync(int matchId, int playerId, bool isStarter, string position)
        {
            // Validar si ya existe ese jugador en el mismo partido
            var exists = await _repository.ExistsByMatchAndPlayerAsync(matchId, playerId);
            if (exists)
                throw new InvalidOperationException("El jugador ya está registrado en la alineación de este partido.");

            var lineup = new MatchLineup
            {
                MatchId = matchId,
                PlayerId = playerId,
                IsStarter = isStarter,
                Position = position,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(lineup);
            return lineup;
        }

        public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAsync(int matchId)
        {
            return await _repository.GetByMatchAsync(matchId);
        }

        public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAndTeamAsync(int matchId, int teamId)
        {
            return await _repository.GetByMatchAndTeamAsync(matchId, teamId);
        }

        public async Task DeleteLineupAsync(int matchId, int lineupId)
        {
            var lineup = await _repository.GetByIdAsync(lineupId);
            if (lineup == null || lineup.MatchId != matchId)
                throw new KeyNotFoundException("No se encontró la alineación para este partido.");

            await _repository.DeleteAsync(lineup);
        }
    }
}
