using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.DAL;

public interface IFlowRepository
{
    IEnumerable<Flow> ReadAllFlows();
    IEnumerable<SingleChoiceQuestion> ReadSingleChoiceQuestionsOfFlow(long flowId);
    IEnumerable<Theme> ReadSubThemasFlow(long flowId);
}