using BL;
using Phygital.Domain.Questionsprocess;
using Phygital.UI_CA.Extension;

namespace Phygital.UI_CA;

public class ConsoleUi
{
    private readonly IFlowManager _flowManager;
    
    public ConsoleUi(IFlowManager flowManager)
    {
        _flowManager = flowManager;
    }
    
    public void Run()
    {
        Console.WriteLine("Get all flows");
        var allFlows = _flowManager.GetAllFlows();
        foreach (var flow in allFlows)
        {
            Console.WriteLine(flow.StringRepresentation());
        }

        // Console.WriteLine("Get all questions of flow 1");
        // var allQuestions = _flowManager.GetAllQuestionsOfFlowById(2);
        // foreach (var question in allQuestions)
        // {
        //     Console.WriteLine(question.Text);
        // }
    }
}