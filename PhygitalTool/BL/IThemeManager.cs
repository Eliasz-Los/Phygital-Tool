using Phygital.Domain.Themas;

namespace Phygital.BL;

public interface IThemeManager
{
    Theme GetThemeById(long id);
    IEnumerable<Theme> GetAllThemas();
    IEnumerable<Theme> GetAllSubThemas();
    IEnumerable<Theme> GetSubThemasFlow(long flowId);
    void AddSubThema(Theme subThema);
    void RemoveTheme(long id);
    void ChangeTheme(long id, string title, string description);
}