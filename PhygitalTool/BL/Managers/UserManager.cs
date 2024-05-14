using Phygital.DAL;
using Phygital.Domain.User;

namespace Phygital.BL.Managers;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<Organisation> GetAllOrganisations()
    {
        return _userRepository.ReadAllOrganisations();
    }

    public Organisation GetOrganisationById(long id)
    {
        return _userRepository.ReadOrganisationById(id);
    }

    public void ChangeOrganisation(long id, string name, string description)
    {
        var organisation = _userRepository.ReadOrganisationById(id);
        organisation.Name = name;
        organisation.Description = description;
        
        _userRepository.UpdateOrganisation(organisation);
    }

    public void RemoveOrganisation(long id)
    {
        _userRepository.DeleteOrganisation(id);
    }

    public void AddOrganisation(Organisation organisation)
    {
        _userRepository.CreateOrganisation(organisation);
    }
}