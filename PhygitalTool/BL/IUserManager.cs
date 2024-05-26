using Phygital.Domain.User;

namespace Phygital.BL;

public interface IUserManager
{
    IEnumerable<Account> GetUsersByOrganisationId(long organisationId);
    void RemoveUser(String id);
    Account GetUserByEmail(string email);
    IEnumerable<Organisation> GetAllOrganisations();
    Organisation GetOrganisationById(long id);
    void ChangeOrganisation(long id, string name, string description);
    void RemoveOrganisation(long id);
    void AddOrganisation(string name, string description);
}