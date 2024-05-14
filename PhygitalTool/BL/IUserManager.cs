using Phygital.Domain.User;

namespace Phygital.BL;

public interface IUserManager
{
    IEnumerable<Organisation> GetAllOrganisations();
    Organisation GetOrganisationById(long id);
    void ChangeOrganisation(long id, string name, string description);
    void RemoveOrganisation(long id);
    void AddOrganisation(Organisation organisation);
}