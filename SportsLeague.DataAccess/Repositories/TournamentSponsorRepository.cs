using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class TournamentSponsorRepository : GenericRepository<TournamentSponsor>, ITournamentSponsorRepository
    {
        private readonly LeagueDbContext _context;

        public TournamentSponsorRepository(LeagueDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TournamentSponsor>> GetByTournamentIdAsync(int tournamentId)
        {
            return await _context.TournamentSponsors
                                 .Include(ts => ts.Sponsor)
                                 .Where(ts => ts.TournamentId == tournamentId)
                                 .ToListAsync();
        }
    }
}
