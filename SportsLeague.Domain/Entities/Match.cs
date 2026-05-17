using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities
{
    public class Match : AuditBase
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; } = string.Empty;
        public int Matchday { get; set; }

        // Cambiado de string a enum
        public MatchStatus Status { get; set; }

        // Relaciones principales
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!;
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; } = null!;
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; } = null!;
        public int RefereeId { get; set; }
        public Referee Referee { get; set; } = null!;

        // Relación 1–1 con resultado
        public MatchResult? Result { get; set; }

        // Colecciones de eventos
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
        public ICollection<Card> Cards { get; set; } = new List<Card>();
        public ICollection<MatchLineup> MatchLineups { get; set; } = new List<MatchLineup>();
    }
}

