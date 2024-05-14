using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto;

public class QuestionDto
{
    public long Id { get; set; }
    public long SubTheme { get; set; }
    public string Text { get; set; }
    public bool isActive { get; set; }
    
    // Type om te kijken om welk type het gaat
    public string Type { get; set; }
}