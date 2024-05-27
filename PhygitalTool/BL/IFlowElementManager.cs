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
    OpenQuestion getOpenQuestionById(long id);
    MultipleChoice getMultipleChoiceQuestionById(long id);
    SingleChoiceQuestion getSingleChoiceQuestionById(long id);
    RangeQuestion getRangeQuestionById(long id);
    IEnumerable<Text> GetTextInfosOfFlowById(long flowId);
    IEnumerable<Image> GetImageInfosOfFlowById(long flowId);
    IEnumerable<Video> GetVideoInfosOfFlowById(long flowId);
    void AddOpenQuestion(OpenQuestion openQuestion);
    Option GetOptionByText(string optionText);
    IEnumerable<OpenQuestion> GetAllOpenQuestionByThemeId(long themeId);
    IEnumerable<OpenQuestion> GetAllOpenQuestionByFlowId(long flowId);
    IEnumerable<SingleChoiceQuestion> GetAllSingleQuestionByFlowId(long flowId);
    IEnumerable<MultipleChoice> GetAllMultipleChoiceQuestionByFlowId(long flowId);
    IEnumerable<RangeQuestion> GetAllRangeQuestionByFlowId(long flowId);
    void AddOption(Option option);
    void AddText(Text text);
    void AddVideo(Video video);
    void AddImage(Image image);
    void AddMultipleChoiceQuestion(MultipleChoice multipleChoiceQuestion);
    void AddSingleChoiceQuestion(SingleChoiceQuestion singleChoiceQuestion);
    void AddRangeQuestion(RangeQuestion rangeQuestion);
    public void RemoveOpenQuestionFromFlow(long questionId);
}