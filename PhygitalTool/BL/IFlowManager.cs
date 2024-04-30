using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Subplatform;
using Phygital.Domain.Themas;

namespace BL;

public interface IFlowManager
{
    IEnumerable<Flow> GetAllFlows();
    Flow GetFlowById(long id);
    Flow GetFlowAndThemeById(long id);
    void ChangeFlow(long id, Flowtype flowtype, bool isOpen, long themeId);
    void RemoveFlow(long id);
}