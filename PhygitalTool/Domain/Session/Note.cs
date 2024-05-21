using System.ComponentModel.DataAnnotations;

namespace Phygital.Domain.Session;

public class Note
{
    public long Id { get; set; }
    [MaxLength(250)]
    public string Comment { get; set; }
    public Participation Participation { get; set; }
}