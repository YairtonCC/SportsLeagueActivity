using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IMatchLineupRepository
    {
        // Obtener alineación por Id
        Task<MatchLineup?> GetByIdAsync(int id);

        // Obtener todas las alineaciones de un partido
        Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId);

        // Obtener alineaciones de un partido filtradas por equipo
        Task<IEnumerable<MatchLineup>> GetByMatchAndTeamAsync(int matchId, int teamId);

        // Validar si un jugador ya está en la alineación de un partido
        Task<bool> ExistsByMatchAndPlayerAsync(int matchId, int playerId);

        // Obtener una alineación específica por partido y jugador
        Task<MatchLineup?> GetByMatchAndPlayerAsync(int matchId, int playerId);

        // Contar titulares de un equipo en un partido
        Task<int> CountStartersByMatchAndTeamAsync(int matchId, int teamId);

        // Agregar alineación
        Task AddAsync(MatchLineup lineup);

        // Eliminar alineación
        Task DeleteAsync(MatchLineup lineup);
    }
}
