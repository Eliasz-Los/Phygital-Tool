using System.ComponentModel.DataAnnotations;
using Phygital.DAL;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace BL;

public class FlowManager : IFlowManager
{
    private readonly IFlowRepository _flowRepository;
    public FlowManager(IFlowRepository flowRepository)
    {
        _flowRepository = flowRepository;
    }


    public IEnumerable<Flow> GetAllFlows()
    {
        return _flowRepository.ReadAllFlows();
    }

    public Flow GetFlowById(long id)
    {
        return _flowRepository.ReadFlowById(id);
    }

    public IEnumerable<SingleChoiceQuestion> GetSingleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _flowRepository.ReadSingleChoiceQuestionsWithOptionsOfFlowById(flowId);
    }

    public IEnumerable<MultipleChoice> GetMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _flowRepository.ReadMultipleChoiceQuestionsWithOptionsOfFlowById(flowId);
    }

    public IEnumerable<RangeQuestion> GetRangeQuestionsWithOptionsOfFlowById(long flowId)
    {
    return _flowRepository.ReadRangeQuestionsWithOptionsOfFlowById(flowId);
    }

    public IEnumerable<OpenQuestion> GetOpenQuestionsWithAnswerOfFlowById(long flowId)
    {
        return _flowRepository.ReadOpenQuestionsWithAnswerOfFlowById(flowId);
    }

    public IEnumerable<Theme> GetSubThemasFlow(long flowId)
    {
        return _flowRepository.ReadSubThemasFlow(flowId);
    }

    public IEnumerable<Theme> GetAllSubThemas()
    {
        return _flowRepository.ReadAllSubThemas();
    }

    public Option GetOptionByText(string optionText)
    {
        return _flowRepository.ReadOptionByText(optionText);
    }

    public Question GetQuestionById(long questionId)
    {
        return _flowRepository.ReadQuestionById(questionId);
    }

    public IEnumerable<Text> GetTextInfosOfFlowById(long flowId)
    {
        return _flowRepository.ReadTextInfosOfFlowById(flowId);
    }

    public IEnumerable<Image> GetImageInfosOfFlowById(long flowId)
    {
        return _flowRepository.ReadImageInfosOfFlowById(flowId);
    }

    public IEnumerable<Video> GetVideoInfosOfFlowById(long flowId)
    {
        return _flowRepository.ReadVideoInfosOfFlowById(flowId);
    }

    public void AddAnswersToFlow(List<Answer> answers)
    {
        foreach (var answer in answers)
        {
            _flowRepository.CreateAnswer(answer);
        }
        
    }
}