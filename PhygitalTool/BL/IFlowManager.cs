using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace BL;

public interface IFlowManager
{

    IEnumerable<Flow> GetAllFlows();
    IEnumerable<SingleChoiceQuestion> GetSingleChoiceQuestionsOfFlow(long flowId);

}