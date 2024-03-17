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
}