using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Repositories.SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Application.Services
{
    public class MatchLineupService : IMatchLineupService
    {
        private readonly IMatchLineupRepository _repository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;

        public MatchLineupService(
            IMatchLineupRepository repository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository)
        {
            _repository = repository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
        }

        public async Task<MatchLineup> AddPlayerToLineupAsync(int matchId, int playerId, bool isStarter, string position)
        {
            // 1. Validar duplicado
            if (await _repository.ExistsByMatchAndPlayerAsync(matchId, playerId))
                throw new InvalidOperationException("El jugador ya está registrado en la alineación de este partido.");

            // 2. Validar jugador existente
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
                throw new KeyNotFoundException($"No se encontró el jugador con ID {playerId}");

            // 3. Validar partido existente
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

            // 4. Validar estado del partido
            if (match.Status == MatchStatus.Finished)
                throw new InvalidOperationException("No se pueden modificar alineaciones de partidos finalizados.");

            // 5. Validar que el jugador pertenezca a los equipos del partido
            if (player.TeamId != match.HomeTeamId && player.TeamId != match.AwayTeamId)
                throw new InvalidOperationException("El jugador no pertenece a los equipos del partido.");

            // 6. Validar máximo 11 titulares por equipo
            if (isStarter)
            {
                if (player.TeamId == null)
                    throw new InvalidOperationException("El jugador no tiene equipo asignado.");

                var startersCount = await _repository.CountStartersByMatchAndTeamAsync(matchId, player.TeamId.Value);
                if (startersCount >= 11)
                    throw new InvalidOperationException("No se permiten más de 11 titulares en el mismo equipo.");
            }

            // 7. Validar posición obligatoria
            if (string.IsNullOrWhiteSpace(position))
                throw new ArgumentException("La posición es obligatoria.", nameof(position));

            // 8. Validar posición válida
            var posicionesValidas = new[] { "Goalkeeper", "Defender", "Midfielder", "Forward" };
            if (!posicionesValidas.Contains(position))
                throw new ArgumentException(
                    $"La posición '{position}' no es válida. Debe ser Goalkeeper, Defender, Midfielder o Forward.",
                    nameof(position));

            // 9. Crear alineación
            var lineup = new MatchLineup
            {
                MatchId = matchId,
                PlayerId = playerId,
                IsStarter = isStarter,
                Position = position,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(lineup);

            // 10. Reconsultar con relaciones incluidas
            var createdLineup = await _repository.GetByIdAsync(lineup.Id);
            return createdLineup!;
        }

        public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAsync(int matchId)
        {
            return await _repository.GetByMatchAsync(matchId);
        }

        public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAndTeamAsync(int matchId, int teamId)
        {
            return await _repository.GetByMatchAndTeamAsync(matchId, teamId);
        }

        public async Task DeleteLineupAsync(int matchId, int playerId)
        {
            var lineup = await _repository.GetByMatchAndPlayerAsync(matchId, playerId);
            if (lineup == null)
                throw new KeyNotFoundException("No se encontró la alineación para este partido.");

            await _repository.DeleteAsync(lineup);
        }
    }
}

