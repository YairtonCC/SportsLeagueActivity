using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchEventService
    {
        Task<MatchResult> RegisterResultAsync(int matchId, MatchResult result); //Registra resultads del partido
        Task<MatchResult?> GetResultByMatchAsync(int matchId); //obtiene resultado en un punto especifico

        Task<Goal> RegisterGoalAsync(int matchId, Goal goal); //Registra el gol
        Task<IEnumerable<Goal>> GetGoalsByMatchAsync(int matchId); //obtiene todos los goles en un partido
        Task DeleteGoalAsync(int goalId); //elimina foles

        Task<Card> RegisterCardAsync(int matchId, Card card);  //Registra tarjetas
        Task<IEnumerable<Card>> GetCardsByMatchAsync(int matchId); //obtiene las tarjetas
        Task DeleteCardAsync(int cardId); //Elimina las tarjetas
    }
}
