using Microsoft.Win32;

namespace SportsLeague.API.DTOs.Request
{
    public class MatchLineupRequestDTO
    {
        public int PlayerId { get; set; }
        //Identificador del jugador que se quiere registrar en el partido
        public bool IsStarter { get; set; }//Indica si el jugador es titular
        public string Position { get; set; } = string.Empty;//Indica la posicion
    }
}
