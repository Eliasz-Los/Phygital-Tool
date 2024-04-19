using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Subplatform;
using Phygital.Domain.Themas;

namespace Phygital.DAL;

public interface IFlowRepository
{
    IEnumerable<Flow> ReadAllFlows();
    Flow ReadFlowById(long id);

    void CreateAnswer(Answer answer);
    void CreateProject(Project project);

    void CreateTheme(Theme theme);

    IEnumerable<SingleChoiceQuestion> ReadSingleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<MultipleChoice> ReadMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<RangeQuestion> ReadRangeQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<OpenQuestion> ReadOpenQuestionsWithAnswerOfFlowById(long flowId);
    IEnumerable<Theme> ReadSubThemasFlow(long flowId);
    IEnumerable<Theme> ReadAllSubThemas();
    Option ReadOptionByText(string optionText);
    Question ReadQuestionById(long questionId);
    IEnumerable<Text> ReadTextInfosOfFlowById(long flowId);
    IEnumerable<Image> ReadImageInfosOfFlowById(long flowId);
    IEnumerable<Video> ReadVideoInfosOfFlowById(long flowId);
    void CreateAnswer(Answer answer);

 
}