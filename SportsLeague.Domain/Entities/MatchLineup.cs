namespace SportsLeague.Domain.Entities
{
    public class MatchLineup
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public bool IsStarter { get; set; }
        public string Position { get; set; } = string.Empty;

        // Propiedades de auditoría
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Relaciones de navegación
        public Match Match { get; set; } = null!;
        public Player Player { get; set; } = null!;
    }
}
