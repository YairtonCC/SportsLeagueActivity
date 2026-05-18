using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities
{
    public class Player : AuditBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Number { get; set; }

        // Cambiado de string a enum
        public PlayerPosition Position { get; set; }

        // Relación con Team
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;

        // Relación con Goals
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();

        // Relación con Cards
        public ICollection<Card> Cards { get; set; } = new List<Card>();

        // Relación con MatchLineups
        public ICollection<MatchLineup> MatchLineups { get; set; } = new List<MatchLineup>();
    }
}
