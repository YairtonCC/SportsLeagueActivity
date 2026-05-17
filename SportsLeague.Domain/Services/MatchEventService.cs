using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Repositories.SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Application.Services
{
    public class MatchEventService : IMatchEventService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMatchEventRepository _eventRepository;

        public MatchEventService(
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository,
            IMatchEventRepository eventRepository)
        {
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _eventRepository = eventRepository;
        }

        public async Task<MatchResult> RegisterResultAsync(int matchId, MatchResult result)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

            if (match.Status == MatchStatus.Finished)
                throw new InvalidOperationException("El resultado ya no puede modificarse, el partido está finalizado.");

            match.Result = result;
            await _matchRepository.UpdateAsync(match);

            return match.Result!;
        }

        public async Task<MatchResult?> GetResultByMatchAsync(int matchId)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            return match?.Result;
        }

        public async Task<Goal> RegisterGoalAsync(int matchId, Goal goal)
        {
            await ValidateEvent(matchId, goal.PlayerId, goal.Minute);
            await _eventRepository.AddGoalAsync(goal);
            return goal;
        }

        public async Task<IEnumerable<Goal>> GetGoalsByMatchAsync(int matchId)
        {
            return await _eventRepository.GetGoalsByMatchAsync(matchId);
        }

        public async Task DeleteGoalAsync(int goalId)
        {
            await _eventRepository.DeleteGoalAsync(goalId);
        }

        public async Task<Card> RegisterCardAsync(int matchId, Card card)
        {
            await ValidateEvent(matchId, card.PlayerId, card.Minute);
            await _eventRepository.AddCardAsync(card);
            return card;
        }

        public async Task<IEnumerable<Card>> GetCardsByMatchAsync(int matchId)
        {
            return await _eventRepository.GetCardsByMatchAsync(matchId);
        }

        public async Task DeleteCardAsync(int cardId)
        {
            await _eventRepository.DeleteCardAsync(cardId);
        }

        // Validaciones comunes
        private async Task ValidateEvent(int matchId, int playerId, int minute)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

            if (match.Status == MatchStatus.Finished)
                throw new InvalidOperationException("No se pueden registrar eventos en partidos finalizados.");

            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
                throw new KeyNotFoundException($"No se encontró el jugador con ID {playerId}");

            if (player.TeamId != match.HomeTeamId && player.TeamId != match.AwayTeamId)
                throw new InvalidOperationException("El jugador no pertenece a los equipos del partido.");

            if (minute < 0 || minute > 90)
                throw new ArgumentException("El minuto del evento debe estar entre 0 y 90.", nameof(minute));
        }
    }
}

