using Phygital.Domain.Questionsprocess.Infos;
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

    OpenQuestion ReadOpenQuestionById(long id);
    SingleChoiceQuestion ReadSingleQuestionById(long id);
    MultipleChoice ReadMultipleChoiceQuestionById(long id);
    RangeQuestion ReadRangeQuestionById(long id);
    
    void CreateOpenQuestion(OpenQuestion openQuestion);
    void CreateOption(Option option);
    void CreateMultipleChoiceQuestion(MultipleChoice multipleChoiceQuestion);
    void CreateSingleChoiceQuestion(SingleChoiceQuestion singleChoiceQuestion);
    void CreateRangeQuestion(RangeQuestion rangeQuestion);
    void CreateImage(Image image);
    void CreateText(Text text);
    void CreateVideo(Video video);

    IEnumerable<Text> ReadTextInfosOfFlowById(long flowId);
    IEnumerable<Image> ReadImageInfosOfFlowById(long flowId);
    IEnumerable<Video> ReadVideoInfosOfFlowById(long flowId);
    Option ReadOptionByText(string optionText);
    IEnumerable<OpenQuestion> ReadAllOpenQuestionByThemeId(long themeId);
    IEnumerable<OpenQuestion> ReadAllOpenQuestionByFlowId(long flowId);
    IEnumerable<MultipleChoice> ReadAllMultipleChoiceQuestionByFlowId(long flowId);
    IEnumerable<SingleChoiceQuestion> ReadAllSingleQuestionByFlowId(long flowId);
    IEnumerable<RangeQuestion> ReadAllRangeQuestionByFlowId(long flowId);
    
    public void UpdateActive(long questionId);

    public void DeleteOpenQuestionFromFlow(long questionId);
}