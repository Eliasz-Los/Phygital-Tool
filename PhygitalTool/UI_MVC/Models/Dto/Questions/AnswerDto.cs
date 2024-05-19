using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto;

public class AnswerDto
{
    public List<Option> ChosenOptions { get; set; }
    public string ChosenAnswer { get; set; }
    /*public long SubThemeId { get; set; }
    public long  FlowId { get; set; }*/
    public long QuestionId { get; set; }
}