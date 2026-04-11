using SportsLeague.Domain.Entities;

public class TournamentSponsor
{
    public int Id { get; set; }
    public int TournamentId { get; set; }
    public int SponsorId { get; set; }
    public decimal ContractAmount { get; set; }
    public DateTime JoinedAt { get; set; }

    // Navigation properties
    public Tournament Tournament { get; set; }
    public Sponsor Sponsor { get; set; }
}
