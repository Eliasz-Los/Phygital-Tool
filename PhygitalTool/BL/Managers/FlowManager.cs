using BL;
using Phygital.DAL;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Subplatform;
using Phygital.Domain.Themas;

namespace Phygital.BL.Managers;

public class FlowManager : IFlowManager
{
    private readonly IFlowRepository _flowRepository;
    private readonly IThemeManager _themeManager;
    public FlowManager(IFlowRepository flowRepository, IThemeManager themeManager)
    {
        _flowRepository = flowRepository;
        _themeManager = themeManager;
    }

    public IEnumerable<Flow> GetAllFlows()
    {
        return _flowRepository.ReadAllFlows();
    }

    public Flow GetFlowById(long id)
    {
        return _flowRepository.ReadFlowById(id);
    }

    public Flow GetFlowAndThemeById(long id)
    {
        return _flowRepository.ReadFlowAndThemeById(id);
    }

    public void AddAnswersToFlow(List<Answer> answers)
    {
        throw new NotImplementedException();
    }

    public void ChangeFlow(long id, Flowtype flowtype, bool isOpen, long themeId)
    {
        var flow = _flowRepository.ReadFlowById(id);
        flow.FlowType = flowtype;
        flow.IsOpen = isOpen;
        
        var theme = _themeManager.GetThemeById(themeId);
        if(theme != null)
            flow.Theme = theme;
        
        _flowRepository.UpdateFlow(flow);
    }

    public void RemoveFlow(long id)
    {
        _flowRepository.DeleteFlow(id);
    }
}