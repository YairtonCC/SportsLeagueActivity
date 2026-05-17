using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.repositories;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Repositories.SportsLeague.Domain.Interfaces.Repositories;
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

        public async Task<IEnumerable<Match>> GetAllByTournamentAsync(int tournamentId)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
            if (tournament == null)
                throw new KeyNotFoundException($"No se encontró el torneo con ID {tournamentId}");

            return await _matchRepository.GetByTournamentWithDetailsAsync(tournamentId);
        }

        public async Task<Match?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Consultando partido con ID: {MatchId}", id);
            return await _matchRepository.GetByIdWithDetailsAsync(id);
        }

        public async Task<Match> CreateAsync(Match match)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(match.TournamentId);
            if (tournament == null)
                throw new KeyNotFoundException($"No se encontró el torneo con ID {match.TournamentId}");

            var homeTeam = await _teamRepository.GetByIdAsync(match.HomeTeamId);
            var awayTeam = await _teamRepository.GetByIdAsync(match.AwayTeamId);
            if (homeTeam == null || awayTeam == null)
                throw new KeyNotFoundException("Uno de los equipos no existe");

            var referee = await _refereeRepository.GetByIdAsync(match.RefereeId);
            if (referee == null)
                throw new KeyNotFoundException($"No se encontró el árbitro con ID {match.RefereeId}");

            _logger.LogInformation("Creando partido en torneo {TournamentId}", match.TournamentId);
            return await _matchRepository.CreateAsync(match);
        }

        public async Task UpdateAsync(int id, Match match)
        {
            var existing = await _matchRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {id}");

            match.Id = id;
            _logger.LogInformation("Actualizando partido con ID: {MatchId}", id);
            await _matchRepository.UpdateAsync(match);
        }

        public async Task DeleteAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {id}");

            _logger.LogInformation("Eliminando partido con ID: {MatchId}", id);
            await _matchRepository.DeleteAsync(id); 
        }


        public async Task UpdateStatusAsync(int id, MatchStatus newStatus)
        {
            var match = await _matchRepository.GetByIdAsync(id);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {id}");

            match.Status = newStatus;
            _logger.LogInformation("Actualizando estado del partido {MatchId} a {Status}", id, newStatus);
            await _matchRepository.UpdateAsync(match);
        }
    }
}

