namespace Phygital.Domain.Questionsprocess.Questions;

public class Option 
{
    public long Id { get; set; }
    //public Question Question { get; set; }

    //Omdat abstaxte klasse Question is, kan deze niet direct worden aangeroepen
    public MultipleChoice MultipleChoice { get; set; }
    public RangeQuestion RangeQuestion { get; set; }
    public SingleChoiceQuestion SingleChoiceQuestion { get; set; }
    public string OptionText { get; set; }
}