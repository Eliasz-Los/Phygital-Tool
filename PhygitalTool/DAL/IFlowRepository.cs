using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Subplatform;
using Phygital.Domain.Themas;

namespace Phygital.DAL;

public interface IFlowRepository
{
    IEnumerable<Flow> ReadAllFlows();
    Flow ReadFlowById(long id);
    void UpdateFlow(Flow flow);
    void DeleteFlow(long id);
}