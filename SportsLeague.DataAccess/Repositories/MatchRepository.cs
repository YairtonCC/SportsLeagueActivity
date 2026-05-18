using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Repositories.SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly LeagueDbContext _context;

        public MatchRepository(LeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Match?> GetByIdAsync(int id)
        {
            return await _context.Matches.FindAsync(id);
        }

        public async Task<Match?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Matches
                .Include(m => m.Tournament)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Referee)
                .Include(m => m.Result)
                .Include(m => m.Goals)
                .Include(m => m.Cards)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            return await _context.Matches
                .Include(m => m.Tournament)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Referee)
                .Include(m => m.Result)
                .ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId)
        {
            return await _context.Matches
                .Where(m => m.TournamentId == tournamentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetByTournamentWithDetailsAsync(int tournamentId)
        {
            return await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Referee)
                .Include(m => m.Result)
                .Include(m => m.Goals)
                .Include(m => m.Cards)
                .Where(m => m.TournamentId == tournamentId)
                .ToListAsync();
        }

       
        public async Task<Match> CreateAsync(Match match)
        {
            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
            return match;
        }

        public async Task UpdateAsync(Match match)
        {
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Matches.AnyAsync(m => m.Id == id);
        }
    }
}
