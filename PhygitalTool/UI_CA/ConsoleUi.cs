using BL;
using Phygital.DAL;
using Phygital.DAL.EF;
using Phygital.Domain.Questionsprocess;
using Phygital.UI_CA.Extension;

namespace Phygital.UI_CA;

public class ConsoleUi
{
    private readonly IFlowManager _flowManager;
    private readonly FlowRepository _flowRepository;
    
    public ConsoleUi(IFlowManager flowManager, FlowRepository repository)
    {
        _flowManager = flowManager;
        _flowRepository = repository;
    }
    
    public void Run()
    {
        Console.WriteLine("Get all flows");
        var allFlows = _flowManager.GetAllFlows();
        foreach (var flow in allFlows)
        {
            Console.WriteLine(flow.StringRepresentation());
        }

        Console.WriteLine("Get scq of flow 1");
        var scq = _flowManager.GetSingleChoiceQuestionsWithOptionsOfFlowById(1);
        foreach (var question in scq)
        {
            Console.WriteLine(question.StringRepresentation());
        }
    }
}