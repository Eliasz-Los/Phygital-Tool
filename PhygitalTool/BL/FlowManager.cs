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
    
    public IEnumerable<SingleChoiceQuestion> GetSingleChoiceQuestionsOfFlow(long flowId)
    {
        return _flowRepository.ReadSingleChoiceQuestionsOfFlow(flowId);
    }

    public IEnumerable<Theme> GetSubThemasFlow(long flowId)
    {
        return _flowRepository.ReadSubThemasFlow(flowId);
    }
}