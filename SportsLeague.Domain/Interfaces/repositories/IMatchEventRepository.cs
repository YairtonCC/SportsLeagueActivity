using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.repositories;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IMatchEventRepository : IGenericRepository<MatchEvent>
    {
        // ── Resultados ──
        Task<MatchResult> AddResultAsync(MatchResult result);
        Task<MatchResult?> GetResultByMatchAsync(int matchId);

        // ── Goles ──
        Task<Goal> AddGoalAsync(Goal goal);
        Task<IEnumerable<Goal>> GetGoalsByMatchAsync(int matchId);
        Task DeleteGoalAsync(int goalId);

        // ── Tarjetas ──
        Task<Card> AddCardAsync(Card card);
        Task<IEnumerable<Card>> GetCardsByMatchAsync(int matchId);
        Task DeleteCardAsync(int cardId);
    }
}
