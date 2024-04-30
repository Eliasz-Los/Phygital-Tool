using Phygital.BL;
using Phygital.BL;
using Phygital.UI_CA.Extension;

namespace Phygital.UI_CA;

public class ConsoleUi
{
    private readonly IFlowManager _flowManager;
    private readonly IFlowElementManager _flowElementManager;
    
    public ConsoleUi(IFlowManager flowManager, IFlowElementManager flowElementManager)
    {
        _flowManager = flowManager;
        _flowElementManager = flowElementManager;
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
        var scq = _flowElementManager.GetSingleChoiceQuestionsWithOptionsOfFlowById(1);
        foreach (var question in scq)
        {
            Console.WriteLine(question.StringRepresentation());
        }
    }
}