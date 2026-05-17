using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly LeagueDbContext _context;

        public PlayerRepository(LeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        public async Task<Player?> GetByIdWithTeamAsync(int id)
        {
            return await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetAllWithTeamAsync()
        {
            return await _context.Players
                .Include(p => p.Team)
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetByTeamAsync(int teamId)
        {
            return await _context.Players
                .Where(p => p.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<Player?> GetByTeamAndNumberAsync(int teamId, int number)
        {
            return await _context.Players
                .FirstOrDefaultAsync(p => p.TeamId == teamId && p.Number == number);
        }

        public async Task<Player> CreateAsync(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Players.AnyAsync(p => p.Id == id);
        }
    }
}
