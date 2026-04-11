using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentSponsorRepository _tournamentSponsorRepository;

        public SponsorService(
            ISponsorRepository sponsorRepository,
            ITournamentRepository tournamentRepository,
            ITournamentSponsorRepository tournamentSponsorRepository)
        {
            _sponsorRepository = sponsorRepository;
            _tournamentRepository = tournamentRepository;
            _tournamentSponsorRepository = tournamentSponsorRepository;
        }

        // CRUD básico
        public async Task<IEnumerable<Sponsor>> GetAllAsync() => await _sponsorRepository.GetAllAsync();

        public async Task<Sponsor?> GetByIdAsync(int id) => await _sponsorRepository.GetByIdAsync(id);

        public async Task<Sponsor> CreateAsync(Sponsor sponsor)
        {
            // Validación: nombre duplicado
            if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
                throw new InvalidOperationException("Sponsor name already exists.");

            // Validación: email válido
            if (!sponsor.ContactEmail.Contains("@"))
                throw new InvalidOperationException("Invalid email format.");

            sponsor.CreatedAt = DateTime.UtcNow;
            return await _sponsorRepository.CreateAsync(sponsor);
        }

        public async Task UpdateAsync(int id, Sponsor sponsor)
        {
            var existing = await _sponsorRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Sponsor not found.");

            existing.Name = sponsor.Name;
            existing.ContactEmail = sponsor.ContactEmail;
            existing.Phone = sponsor.Phone;
            existing.WebsiteUrl = sponsor.WebsiteUrl;
            existing.Category = sponsor.Category;
            existing.UpdatedAt = DateTime.UtcNow;

            await _sponsorRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _sponsorRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Sponsor not found.");

            await _sponsorRepository.DeleteAsync(existing);
        }

        // Relación con torneos
        public async Task<TournamentSponsor> LinkToTournamentAsync(int sponsorId, int tournamentId, decimal contractAmount)
        {
            var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
            if (sponsor == null)
                throw new KeyNotFoundException("Sponsor not found.");

            var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
            if (tournament == null)
                throw new KeyNotFoundException("Tournament not found.");

            if (contractAmount <= 0)
                throw new InvalidOperationException("Contract amount must be greater than 0.");

            // Validación: no duplicar vínculo
            var existing = await _tournamentSponsorRepository
                .GetByTournamentIdAsync(tournamentId);

            if (existing.Any(ts => ts.SponsorId == sponsorId))
                throw new InvalidOperationException("Sponsor already linked to this tournament.");

            var tournamentSponsor = new TournamentSponsor
            {
                TournamentId = tournamentId,
                SponsorId = sponsorId,
                ContractAmount = contractAmount,
                JoinedAt = DateTime.UtcNow
            };

            return await _tournamentSponsorRepository.CreateAsync(tournamentSponsor);
        }

        public async Task<IEnumerable<TournamentSponsor>> GetTournamentsBySponsorAsync(int sponsorId)
        {
            var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
            if (sponsor == null)
                throw new KeyNotFoundException("Sponsor not found.");

            return sponsor.TournamentSponsors;
        }

        public async Task UnlinkFromTournamentAsync(int sponsorId, int tournamentId)
        {
            var existing = await _tournamentSponsorRepository
                .GetByTournamentIdAsync(tournamentId);

            var link = existing.FirstOrDefault(ts => ts.SponsorId == sponsorId);
            if (link == null)
                throw new KeyNotFoundException("Link not found.");

            await _tournamentSponsorRepository.DeleteAsync(link);
        }
    }
}
