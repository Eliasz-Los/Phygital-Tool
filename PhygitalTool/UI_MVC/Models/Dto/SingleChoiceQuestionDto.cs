using Phygital.Domain.Questionsprocess.Questions;

namespace Phygital.UI_MVC.Models.Dto;

public class SingleChoiceQuestionDto
{
    public string Text { get; set; }
    public ICollection<Option> Options { get; set; }
}