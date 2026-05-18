using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities
{
    public class MatchEvent : AuditBase
    {
        public int Id { get; set; }

        // Relación con el partido
        public int MatchId { get; set; }
        public Match Match { get; set; } = null!;

        // Relación con el jugador
        public int PlayerId { get; set; }
        public Player Player { get; set; } = null!;

        // Tipo de evento (ej: Gol, Tarjeta, Sustitución)
        public EventType EventType { get; set; }

        // Minuto del evento
        public int Minute { get; set; }
    }
}
