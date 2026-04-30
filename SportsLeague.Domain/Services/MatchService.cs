using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.repositories;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services; 

namespace SportsLeague.Domain.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentTeamRepository _tournamentTeamRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IRefereeRepository _refereeRepository;
        private readonly ILogger<MatchService> _logger;

        public MatchService(
            IMatchRepository matchRepository,
            ITournamentRepository tournamentRepository,
            ITournamentTeamRepository tournamentTeamRepository,
            ITeamRepository teamRepository,
            IRefereeRepository refereeRepository,
            ILogger<MatchService> logger)
        {
            _matchRepository = matchRepository;
            _tournamentRepository = tournamentRepository;
            _tournamentTeamRepository = tournamentTeamRepository;
            _teamRepository = teamRepository;
            _refereeRepository = refereeRepository;
            _logger = logger;
        }

        // Métodos de IMatchService (ya implementados en tu código)
        public async Task<IEnumerable<Match>> GetAllByTournamentAsync(int tournamentId)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
            if (tournament == null)
                throw new KeyNotFoundException($"No se encontró el torneo con ID {tournamentId}");

            return await _matchRepository.GetByTournamentWithDetailsAsync(tournamentId);
        }

        public async Task<Match?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving match with ID: {MatchId}", id);
            return await _matchRepository.GetByIdWithDetailsAsync(id);
        }

        public async Task<Match> CreateAsync(Match match)
        {
            // tu lógica de validación y creación...
            return await _matchRepository.CreateAsync(match);
        }

        public async Task UpdateAsync(int id, Match match)
        {
            // tu lógica de actualización...
            await _matchRepository.UpdateAsync(match);
        }

        public async Task DeleteAsync(int id)
        {
            // tu lógica de eliminación...
            await _matchRepository.DeleteAsync(id);
        }

        public async Task UpdateStatusAsync(int id, MatchStatus newStatus)
        {
            // tu lógica de actualización de estado...
            var match = await _matchRepository.GetByIdAsync(id);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {id}");

            match.Status = newStatus;
            await _matchRepository.UpdateAsync(match);
        }
    }
}
