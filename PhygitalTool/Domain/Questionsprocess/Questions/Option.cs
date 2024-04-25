namespace Phygital.Domain.Questionsprocess.Questions;

public class Option 
{
    public long Id { get; set; }
    //dit kan maar dan hebben we specifiek fk naar de juiste tabel nodig
   // public Question Question { get; set; }

    //Dit is makkelijker om te gebruiken, sinds nie elke question heeft een option
    public MultipleChoice MultipleChoice { get; set; }
    public RangeQuestion RangeQuestion { get; set; }
    public SingleChoiceQuestion SingleChoiceQuestion { get; set; }
    
    public string OptionText { get; set; }
    
    //om answer te koppelen aan GEKOZEN optie(s)
    public Answer Answer { get; set; }
}