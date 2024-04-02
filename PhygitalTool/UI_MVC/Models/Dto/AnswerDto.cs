using System.Collections.ObjectModel;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto;

public class AnswerDto
{
    public List<Option> ChosenOptions { get; set; }
    public string ChosenAnswer { get; set; }
    public Theme SubTheme { get; set; }
    public Flow  Flow { get; set; }
}