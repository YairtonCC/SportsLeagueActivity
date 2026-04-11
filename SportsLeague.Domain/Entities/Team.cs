using SportsLeague.Domain.Entities;

public class Team : AuditBase
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Stadium { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;

    // Nueva propiedad
    public DateTime FoundedDate { get; set; }

    // Relaciones
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>();
}
