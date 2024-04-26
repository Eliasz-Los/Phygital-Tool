using Phygital.Domain.Themas;

namespace Phygital.DAL.EF;

public class ThemeRepository : IThemeRepository
{
    private readonly PhygitalDbContext _dbContext;

    public ThemeRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void CreateTheme(Theme theme)
    {
        _dbContext.Themas.Add(theme);
    }

    public Theme ReadThemeById(long id)
    {
        return _dbContext.Themas.Find(id);
    }

    public IEnumerable<Theme> ReadAllThemas()
    {
        return _dbContext.Themas.Select(t => t);
    }

    public IEnumerable<Theme> ReadAllSubThemas()
    {
        return _dbContext.Themas.Select(t => t);
    }

    public IEnumerable<Theme> ReadSubThemasFlow(long flowId)
    {
        return _dbContext.Themas.Where(t => t.Flows.Any(f => f.Id == flowId)).Select(t => t);
    }

    public void UpdateTheme(Theme theme)
    {
        _dbContext.Themas.Update(theme);
    }

    public void DeleteTheme(long id)
    {
        var themaToDelete = _dbContext.Themas.Find(id);
        _dbContext.Themas.Remove(themaToDelete);    }
}