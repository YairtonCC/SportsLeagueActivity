using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class MatchLineupRepository : IMatchLineupRepository
    {
        private readonly LeagueDbContext _context;

        public MatchLineupRepository(LeagueDbContext context)
        {
            _context = context;
        }

        public async Task<MatchLineup?> GetByIdAsync(int id)
        {
            return await _context.MatchLineups
                .Include(ml => ml.Player)
                    .ThenInclude(p => p.Team)
                .Include(ml => ml.Match)
                .FirstOrDefaultAsync(ml => ml.Id == id);
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId)
        {
            return await _context.MatchLineups
                .Include(ml => ml.Player)
                    .ThenInclude(p => p.Team)
                .Include(ml => ml.Match)
                .Where(ml => ml.MatchId == matchId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAndTeamAsync(int matchId, int teamId)
        {
            return await _context.MatchLineups
                .Include(ml => ml.Player)
                    .ThenInclude(p => p.Team)
                .Include(ml => ml.Match)
                .Where(ml => ml.MatchId == matchId && ml.Player.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<bool> ExistsByMatchAndPlayerAsync(int matchId, int playerId)
        {
            return await _context.MatchLineups
                .AnyAsync(ml => ml.MatchId == matchId && ml.PlayerId == playerId);
        }

        public async Task<MatchLineup?> GetByMatchAndPlayerAsync(int matchId, int playerId)
        {
            return await _context.MatchLineups
                .Include(ml => ml.Player)
                    .ThenInclude(p => p.Team)
                .Include(ml => ml.Match)
                .FirstOrDefaultAsync(ml => ml.MatchId == matchId && ml.PlayerId == playerId);
        }

        public async Task<int> CountStartersByMatchAndTeamAsync(int matchId, int teamId)
        {
            return await _context.MatchLineups
                .Include(ml => ml.Player) 
                .Where(ml => ml.MatchId == matchId
                             && ml.IsStarter
                             && ml.Player.TeamId == teamId)
                .CountAsync();
        }

        public async Task AddAsync(MatchLineup lineup)
        {
            await _context.MatchLineups.AddAsync(lineup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MatchLineup lineup)
        {
            _context.MatchLineups.Remove(lineup);
            await _context.SaveChangesAsync();
        }
    }
}

