using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;

namespace Phygital.BL;

public interface IFlowManager
{
    IEnumerable<Flow> GetAllFlows();
    Flow GetFlowById(long id);
    Flow GetFlowAndThemeById(long id);
    void ChangeFlow(long id, Flowtype flowtype, bool isOpen, long themeId);
    void RemoveFlow(long id);
    void AddFlow(Flow flow);
}