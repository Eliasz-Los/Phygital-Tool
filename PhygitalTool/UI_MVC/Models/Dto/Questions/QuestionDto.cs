using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto;

public class QuestionDto
{
    public long Id { get; set; }
    public Theme SubTheme { get; set; }
    public string Text { get; set; }
    
    // Om te zien om welke type van question het gaat
    public string Type { get; set; }
}