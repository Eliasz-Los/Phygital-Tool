using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Subplatform;
using Phygital.Domain.Themas;

namespace BL;

public interface IFlowManager
{

    IEnumerable<Flow> GetAllFlows();
    Flow GetFlowById(long id);
    IEnumerable<SingleChoiceQuestion> GetSingleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<MultipleChoice> GetMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<RangeQuestion> GetRangeQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<OpenQuestion> GetOpenQuestionsWithAnswerOfFlowById(long flowId);
    IEnumerable<Theme> GetSubThemasFlow(long flowId);
    IEnumerable<Theme> GetAllSubThemas();
    Option GetOptionByText(string optionText);
    void AddProject(Project project);
    void AddSubThema(Theme subThema);
    IEnumerable<Theme> GetThemeById(long id);

    Question GetQuestionById(long questionId);
    IEnumerable<Text> GetTextInfosOfFlowById(long flowId);
    IEnumerable<Image> GetImageInfosOfFlowById(long flowId);
    IEnumerable<Video> GetVideoInfosOfFlowById(long flowId);
    void AddAnswersToFlow(List<Answer> answers);
}