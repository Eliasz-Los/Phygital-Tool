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

    public void RemoveTheme(long id)
    {
        _themeRepository.DeleteTheme(id);
    }

    public void ChangeTheme(long id, string title, string description)
    {
        var theme = _themeRepository.ReadThemeById(id);
        theme.Title = title;
        theme.Description = description;
        _themeRepository.UpdateTheme(theme);
    }

    public Theme GetThemeById(long id)
    {
        return _themeRepository.ReadThemeById(id);
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