using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class MatchEventRepository : IMatchEventRepository
    {
        private readonly LeagueDbContext _context;

        public MatchEventRepository(LeagueDbContext context)
        {
            _context = context;
        }

        // ── Métodos genéricos de IGenericRepository<MatchEvent> ──
        public async Task<IEnumerable<MatchEvent>> GetAllAsync()
        {
            return await _context.MatchEvents.ToListAsync();
        }

        public async Task<MatchEvent?> GetByIdAsync(int id)
        {
            return await _context.MatchEvents.FindAsync(id);
        }

        public async Task<MatchEvent> CreateAsync(MatchEvent entity)
        {
            _context.MatchEvents.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(MatchEvent entity)
        {
            _context.MatchEvents.Update(entity);
            await _context.SaveChangesAsync();
        }

       
        public async Task DeleteAsync(MatchEvent entity)
        {
            _context.MatchEvents.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var matchEvent = await _context.MatchEvents.FindAsync(id);
            if (matchEvent != null)
            {
                _context.MatchEvents.Remove(matchEvent);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.MatchEvents.AnyAsync(me => me.Id == id);
        }

        // ── Resultados ──
        public async Task<MatchResult> AddResultAsync(MatchResult result)
        {
            _context.MatchResults.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<MatchResult?> GetResultByMatchAsync(int matchId)
        {
            return await _context.MatchResults
                .FirstOrDefaultAsync(r => r.MatchId == matchId);
        }

        // ── Goles ──
        public async Task<Goal> AddGoalAsync(Goal goal)
        {
            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<IEnumerable<Goal>> GetGoalsByMatchAsync(int matchId)
        {
            return await _context.Goals
                .Where(g => g.MatchId == matchId)
                .ToListAsync();
        }

        public async Task DeleteGoalAsync(int goalId)
        {
            var goal = await _context.Goals.FindAsync(goalId);
            if (goal != null)
            {
                _context.Goals.Remove(goal);
                await _context.SaveChangesAsync();
            }
        }

        // ── Tarjetas ──
        public async Task<Card> AddCardAsync(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<IEnumerable<Card>> GetCardsByMatchAsync(int matchId)
        {
            return await _context.Cards
                .Where(c => c.MatchId == matchId)
                .ToListAsync();
        }

        public async Task DeleteCardAsync(int cardId)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
            }
        }
    }
}
