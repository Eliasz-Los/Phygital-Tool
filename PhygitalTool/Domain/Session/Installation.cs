using System.ComponentModel.DataAnnotations;

namespace Phygital.Domain.Session;

public class Installation
{
    public long Id { get; set; }
    [MinLength(3)]
    public string Name { get; set; }
    [Range(1000, 9999)]
    public int PostalCode { get; set; }
    [MinLength(3)]
    public string Street { get; set; }
    [Range(1, 9999)]
    public int StreetNumber { get; set; }
    public ICollection<Session> Sessions { get; set; }
}