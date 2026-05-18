using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchLineupService
    {
        Task<MatchLineup> AddPlayerToLineupAsync(int matchId, int playerId, bool isStarter, string position); //agregar jugador a la alineacion del partido
        Task<IEnumerable<MatchLineup>> GetLineupByMatchAsync(int matchId); //obtener alineacion completa    
        Task<IEnumerable<MatchLineup>> GetLineupByMatchAndTeamAsync(int matchId, int teamId);//obtener alineacion filtrada
        Task DeleteLineupAsync(int matchId, int lineupId); //Eliminar a un jugador de la alineacion
    }
}
