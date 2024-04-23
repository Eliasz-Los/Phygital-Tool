using Phygital.Domain.Themas;

namespace Phygital.BL;

public interface IThemeManager
{
    IEnumerable<Theme> GetThemeById(long id);
    IEnumerable<Theme> GetAllThemas();
    IEnumerable<Theme> GetAllSubThemas();
    IEnumerable<Theme> GetSubThemasFlow(long flowId);
    void AddSubThema(Theme subThema);
}