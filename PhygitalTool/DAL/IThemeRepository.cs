using Phygital.Domain.Themas;

namespace Phygital.DAL;

public interface IThemeRepository
{
    void CreateTheme(Theme theme);
    Theme ReadThemeById(long id);
    IEnumerable<Theme> ReadAllThemas();
    IEnumerable<Theme> ReadAllSubThemas();
    IEnumerable<Theme> ReadSubThemasFlow(long flowId);
    void DeleteThemeById(long id);
    void UpdateTheme(Theme theme);
}