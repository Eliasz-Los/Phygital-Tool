using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Infos;
using Phygital.Domain.Questionsprocess.Questions;

namespace Phygital.BL;

public interface IFlowElementManager
{
    IEnumerable<SingleChoiceQuestion> GetSingleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<MultipleChoice> GetMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<RangeQuestion> GetRangeQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<OpenQuestion> GetOpenQuestionsWithAnswerOfFlowById(long flowId);
    Question GetQuestionById(long questionId);
    IEnumerable<Text> GetTextInfosOfFlowById(long flowId);
    IEnumerable<Image> GetImageInfosOfFlowById(long flowId);
    IEnumerable<Video> GetVideoInfosOfFlowById(long flowId);
    void AddOpenQuestion(OpenQuestion openQuestion);
    Option GetOptionByText(string optionText);
    IEnumerable<OpenQuestion> GetAllOpenQuestionByThemeId(long themeId);
    void AddOption(Option option);
    void AddMultipleChoiceQuestion(MultipleChoice multipleChoiceQuestion);
    void AddSingleChoiceQuestion(SingleChoiceQuestion singleChoiceQuestion);
    void AddRangeQuestion(RangeQuestion rangeQuestion);
}