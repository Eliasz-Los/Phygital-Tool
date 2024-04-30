using Phygital.Domain.Questionsprocess;

public interface IFlowRepository
{
    IEnumerable<Flow> ReadAllFlows();
    Flow ReadFlowById(long id);
    Flow ReadFlowAndThemeById(long id);
    void UpdateFlow(Flow flow);
    void DeleteFlow(long id);
}