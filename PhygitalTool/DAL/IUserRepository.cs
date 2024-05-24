using Phygital.Domain.User;

namespace Phygital.DAL;

public interface IUserRepository
{
    IEnumerable<Account> ReadUsersByOrganisationId(long organisationId);
    void DeleteUser(long id);
    Account ReadUserByEmail(string email);
    IEnumerable<Organisation> ReadAllOrganisations();
    Organisation ReadOrganisationById(long id);
    void UpdateOrganisation(Organisation organisation);
    void DeleteOrganisation(long id);
    void CreateOrganisation(Organisation organisation);
}