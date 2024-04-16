using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.DAL;

public interface IFlowRepository
{
    IEnumerable<Flow> ReadAllFlows();
    Flow ReadFlowById(long id);
    IEnumerable<SingleChoiceQuestion> ReadSingleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<MultipleChoice> ReadMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<RangeQuestion> ReadRangeQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<OpenQuestion> ReadOpenQuestionsWithAnswerOfFlowById(long flowId);
    IEnumerable<Theme> ReadSubThemasFlow(long flowId);
    IEnumerable<Theme> ReadAllSubThemas();
    Option ReadOptionByText(string optionText);
    Question ReadQuestionById(long questionId);
    IEnumerable<Text> ReadTextInfosOfFlowById(long flowId);
    void CreateAnswer(Answer answer);
}