using Microsoft.EntityFrameworkCore;
using Phygital.Domain.User;

namespace Phygital.DAL.EF;

public class UserRepository : IUserRepository
{
    private readonly PhygitalDbContext _dbContext;

    public UserRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Organisation> ReadAllOrganisations()
    {
        return _dbContext.Organisations;
    }

    public Organisation ReadOrganisationById(long id)
    {
        return _dbContext.Organisations.Find(id);
    }

    public void UpdateOrganisation(Organisation organisation)
    {
        _dbContext.Organisations.Update(organisation);
    }

    public void DeleteOrganisation(long id)
    {
        var organisationToDelete = _dbContext.Organisations.Find(id);
        
        var relatedAccounts = _dbContext.Accounts.Where(a => a.Organisation.id == id);
        foreach (var account in relatedAccounts)
        {
            account.Organisation = null; 
        }
        
        _dbContext.Organisations.Remove(organisationToDelete);
    }

    public void CreateOrganisation(Organisation organisation)
    {
        _dbContext.Organisations.Add(organisation);
    }
}