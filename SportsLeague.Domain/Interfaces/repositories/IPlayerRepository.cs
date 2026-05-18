using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player?> GetByIdAsync(int id);
        Task<Player?> GetByIdWithTeamAsync(int id);   
        Task<IEnumerable<Player>> GetAllAsync();
        Task<IEnumerable<Player>> GetAllWithTeamAsync(); 
        Task<IEnumerable<Player>> GetByTeamAsync(int teamId);
        Task<Player?> GetByTeamAndNumberAsync(int teamId, int number);

        Task<Player> CreateAsync(Player player);   
        Task UpdateAsync(Player player);           
        Task DeleteAsync(int id);                  
        Task<bool> ExistsAsync(int id);            
    }
}
