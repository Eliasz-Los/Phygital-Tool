using Phygital.DAL;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;

namespace Phygital.BL.Managers;

public class FlowElementManager : IFlowElementManager
{
    private readonly IFlowElementRepository _flowElementRepository;

    public FlowElementManager(IFlowElementRepository flowElementRepository)
    {
        _flowElementRepository = flowElementRepository;
    }

    public IEnumerable<SingleChoiceQuestion> GetSingleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _flowElementRepository.ReadSingleChoiceQuestionsWithOptionsOfFlowById(flowId);
    }

    public IEnumerable<MultipleChoice> GetMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _flowElementRepository.ReadMultipleChoiceQuestionsWithOptionsOfFlowById(flowId);
    }

    public IEnumerable<RangeQuestion> GetRangeQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _flowElementRepository.ReadRangeQuestionsWithOptionsOfFlowById(flowId);
    }

    public IEnumerable<OpenQuestion> GetOpenQuestionsWithAnswerOfFlowById(long flowId)
    {
        return _flowElementRepository.ReadOpenQuestionsWithAnswerOfFlowById(flowId);
    }

    public Question GetQuestionById(long questionId)
    {
        return _flowElementRepository.ReadQuestionById(questionId);
    }

    public IEnumerable<Text> GetTextInfosOfFlowById(long flowId)
    {
        return _flowElementRepository.ReadTextInfosOfFlowById(flowId);
    }

    public IEnumerable<Image> GetImageInfosOfFlowById(long flowId)
    {
        return _flowElementRepository.ReadImageInfosOfFlowById(flowId);
    }

    public IEnumerable<Video> GetVideoInfosOfFlowById(long flowId)
    {
        return _flowElementRepository.ReadVideoInfosOfFlowById(flowId);
    }

    public void AddOpenQuestion(OpenQuestion openQuestion)
    {
        _flowElementRepository.CreateOpenQuestion(openQuestion);
    }

    public Option GetOptionByText(string optionText)
    {
        return _flowElementRepository.ReadOptionByText(optionText);
    }

    public IEnumerable<OpenQuestion> GetAllOpenQuestionByThemeId(long themeId)
    {
        return _flowElementRepository.ReadAllOpenQuestionByThemeId(themeId);
    }

    public void AddOption(Option option)
    {
        _flowElementRepository.CreateOption(option);
    }

    public void AddMultipleChoiceQuestion(MultipleChoice multipleChoiceQuestion)
    {
        _flowElementRepository.CreateMultipleChoiceQuestion(multipleChoiceQuestion);
    }

    public void AddSingleChoiceQuestion(SingleChoiceQuestion singleChoiceQuestion)
    {
        _flowElementRepository.CreateSingleChoiceQuestion(singleChoiceQuestion);
    }

    public void AddRangeQuestion(RangeQuestion rangeQuestion)
    {
        _flowElementRepository.CreateRangeQuestion(rangeQuestion);
    }
}