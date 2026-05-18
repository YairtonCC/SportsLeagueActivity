namespace SportsLeague.API.DTOs.Request;

public class MatchResultRequestDTO
{
    public int HomeGoals { get; set; }//Goles anotados por el equipo local
    public int AwayGoals { get; set; } //Goles x el equipo visitante
    public string? Observations { get; set; } //comentarios u observaciones
}
