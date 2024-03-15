using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

// The Answer class represents the answer a user has given to a specific question
public class Answer : FlowElement
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }

    [MaxLength(255)]
    public string Text { get; set; }
    
    // Link to the question
    
    // Misschien mag dit weg omdat we nu de abstracte klasse gebruiken
    public OpenQuestion OpenQuestion { get; set; }
    public MultipleChoice MultipleChoice { get; set; }
    public RangeQuestion RangeQuestion { get; set; }
    public SingleChoiceQuestion SingleChoiceQuestion { get; set; }
    
    //foreign key because answer is dependent on the question
  //  public long OpenQuestionId { get; set; }
  
}