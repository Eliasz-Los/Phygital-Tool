using Phygital.Domain.Questionsprocess;

namespace Phygital.DAL;
public interface IFlowRepository
{
    IEnumerable<Flow> ReadAllFlows();
    Flow ReadFlowById(long id);
    Flow ReadFlowAndThemeById(long id);
    void UpdateFlow(Flow flow);
    void DeleteFlow(long id);
    void CreateFlow(Flow flow);
}