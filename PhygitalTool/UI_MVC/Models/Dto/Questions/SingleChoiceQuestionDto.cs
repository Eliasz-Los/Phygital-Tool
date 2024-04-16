using Phygital.Domain.Questionsprocess.Questions;

namespace Phygital.UI_MVC.Models.Dto;

public class SingleChoiceQuestionDto
{
    public long Id { get; set; }
    //public Theme SubTheme { get; set; }
    public string Text { get; set; }
    public bool Active { get; set; }
    public int SequenceNumber { get; set; }
    public List<String> Options { get; set; }
}