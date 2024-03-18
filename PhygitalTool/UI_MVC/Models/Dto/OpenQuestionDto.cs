using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto;

public class OpenQuestionDto
{
    public Theme SubTheme { get; set; }
    public string Text { get; set; }
    public bool Active { get; set; }
    public int SequenceNumber { get; set; }
    //verandert van Answer naar string
    public string Answer { get; set; }
}