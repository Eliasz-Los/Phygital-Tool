using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Questions;

public abstract class Question : FlowElement
{
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }
    
}