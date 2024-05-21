using System.ComponentModel.DataAnnotations;

namespace Phygital.Domain.User;

public class Organisation
{
    public long id { get; set; }
    [MinLength(3)]
    public string Name { get; set; }
    [MaxLength(250)]
    public string Description { get; set; }
    public ICollection<Account> Accounts { get; set; }
}