using BL;
using Phygital.DAL;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;

namespace Phygital.BL;

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

    public void ChangeFlow(long id, Flowtype flowtype, bool isOpen, long themeId)
    {
        throw new NotImplementedException();
    }

    public void RemoveFlow(long id)
    {
        throw new NotImplementedException();
    }
}