namespace SportsLeague.Domain.Entities
{
    public class TournamentSponsor : AuditBase
    {
        public int Id { get; set; }

        // Relación con el torneo
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!;

        // Relación con el sponsor
        public int SponsorId { get; set; }
        public Sponsor Sponsor { get; set; } = null!;

        // Propiedades que tu DbContext espera
        public decimal ContractAmount { get; set; }   // Monto del contrato
        public DateTime JoinedAt { get; set; }        // Fecha en que se unió

        // Auditoría heredada de AuditBase: CreatedAt, UpdatedAt, etc.
    }
}
