namespace Phygital.UI_MVC.Models.Dto.Session;

public class ParticipationDto
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int AmountOfParticipants { get; set; }
}