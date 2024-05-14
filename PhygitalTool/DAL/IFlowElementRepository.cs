using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.DAL;

public interface IFlowElementRepository
{
    Question ReadQuestionById(long questionId);
    IEnumerable<SingleChoiceQuestion> ReadSingleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<MultipleChoice> ReadMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<RangeQuestion> ReadRangeQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<OpenQuestion> ReadOpenQuestionsWithAnswerOfFlowById(long flowId);
    IEnumerable<OpenQuestion> ReadAllOpenQuestionsByTheme(Theme subTheme);
    IEnumerable<SingleChoiceQuestion> ReadAllSingleChoiceQuestionsByTheme(Theme subTheme);
    IEnumerable<RangeQuestion> ReadAllRangeQuestionsByTheme(Theme subTheme);
    void CreateOpenQuestion(OpenQuestion openQuestion);

    IEnumerable<Text> ReadTextInfosOfFlowById(long flowId);
    IEnumerable<Image> ReadImageInfosOfFlowById(long flowId);
    IEnumerable<Video> ReadVideoInfosOfFlowById(long flowId);
    Option ReadOptionByText(string optionText);
    IEnumerable<OpenQuestion> ReadAllOpenQuestionByThemeId(long themeId);
}