using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;

namespace Phygital.DAL;

public interface IFlowElementRepository
{
    Question ReadQuestionById(long questionId);
    IEnumerable<SingleChoiceQuestion> ReadSingleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<MultipleChoice> ReadMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<RangeQuestion> ReadRangeQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<OpenQuestion> ReadOpenQuestionsWithAnswerOfFlowById(long flowId);
    
    IEnumerable<Text> ReadTextInfosOfFlowById(long flowId);
    IEnumerable<Image> ReadImageInfosOfFlowById(long flowId);
    IEnumerable<Video> ReadVideoInfosOfFlowById(long flowId);
    Option ReadOptionByText(string optionText);
}