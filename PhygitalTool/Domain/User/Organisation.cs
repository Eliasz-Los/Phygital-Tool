namespace Phygital.Domain.User;

public class Organisation
{
    public long id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Account> Accounts { get; set; }
}