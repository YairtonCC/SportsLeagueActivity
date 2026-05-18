namespace SportsLeague.API.DTOs.Response
{
    public class MatchLineupResponseDTO
    {
        public int Id { get; set; } //Identificador único del registro de alineación en la base de datos.
        public int MatchId { get; set; } //Identificador del partido al que pertenece la alineación
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public bool IsStarter { get; set; }
        public string Position { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
