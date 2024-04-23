using Phygital.DAL;
using Phygital.Domain.Themas;

namespace Phygital.BL.Managers;

public class ThemeManager : IThemeManager
{
    private readonly IThemeRepository _themeRepository;

    public ThemeManager(IThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }

    public IEnumerable<Theme> GetThemeById(long themeid)
    {
        return _themeRepository.DeleteThemeById(themeid);
    }

    public IEnumerable<Theme> GetAllThemas()
    {
        return _themeRepository.ReadAllThemas();
    }

    public IEnumerable<Theme> GetAllSubThemas()
    {
        return _themeRepository.ReadAllSubThemas();
    }

    public IEnumerable<Theme> GetSubThemasFlow(long flowId)
    {
        return _themeRepository.ReadSubThemasFlow(flowId);
    }

    public void AddSubThema(Theme subThema)
    {
        _themeRepository.CreateTheme(subThema);
    }
}