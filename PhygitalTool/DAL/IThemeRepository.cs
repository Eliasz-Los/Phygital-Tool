using Phygital.Domain.Themas;

namespace Phygital.DAL;

public interface IThemeRepository
{
    void CreateTheme(Theme theme);
    IEnumerable<Theme> ReadAllThemas();
    IEnumerable<Theme> ReadAllSubThemas();
    IEnumerable<Theme> ReadSubThemasFlow(long flowId);
    IEnumerable<Theme> DeleteThemeById(long id);
}