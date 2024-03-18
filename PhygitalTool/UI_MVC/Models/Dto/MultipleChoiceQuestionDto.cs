using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto;

public class MultipleChoiceQuestionDto
{
    public Theme SubTheme { get; set; }
    public string Text { get; set; }
    public bool Active { get; set; }
    public int SequenceNumber { get; set; }
    
    public List<String> Options { get; set; }
}