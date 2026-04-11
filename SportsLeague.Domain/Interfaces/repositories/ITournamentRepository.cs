using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.repositories;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentRepository : IGenericRepository<Tournament>
    {
        Task<IEnumerable<Tournament>> GetByStatusAsync(TournamentStatus status);
        Task<Tournament?> GetByIdWithTeamsAsync(int id);
    }
}