using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.repositories;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IRefereeRepository : IGenericRepository<Referee>
    {
        Task<IEnumerable<Referee>> GetByNationalityAsync(string nationality);
    }
}
