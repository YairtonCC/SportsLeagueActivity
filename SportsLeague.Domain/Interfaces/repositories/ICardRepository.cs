using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.repositories;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface ICardRepository : IGenericRepository<Card>
{
    Task<IEnumerable<Card>> GetByMatchAsync(int matchId);
    Task<IEnumerable<Card>> GetByMatchWithDetailsAsync(int matchId);
}
