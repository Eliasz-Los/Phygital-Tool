using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

// The Answer class represents the answer a user has given to a specific question
public class Answer
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }
    
    public ICollection<Option> ChosenOptions { get; set; }
    [MaxLength(250)]
    public string ChosenAnswer { get; set; }
    
    // Link to the question
    
    // Misschien mag dit weg omdat we nu de abstracte klasse gebruiken
    public OpenQuestion OpenQuestion { get; set; }
    public MultipleChoice MultipleChoice { get; set; }
    public RangeQuestion RangeQuestion { get; set; }
    public SingleChoiceQuestion SingleChoiceQuestion { get; set; }
}