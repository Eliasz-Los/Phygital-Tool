namespace Phygital.Domain.Questionsprocess.Questions;

public class Option
{
    public long Id { get; set; }
    public Question Question { get; set; }
    public string OptionText { get; set; }
}