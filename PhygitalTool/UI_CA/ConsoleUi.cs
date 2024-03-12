using BL;
using Phygital.Domain.Questionsprocess;
using Phygital.UI_CA.Extension;

namespace Phygital.UI_CA;

public class ConsoleUi
{
    private readonly IManager _manager;
    
    public ConsoleUi(IManager manager)
    {
        _manager = manager;
    }
    
    public void Run()
    {
        Console.WriteLine("Get all flows");
        var allFlows = _manager.GetAllFlows();
        foreach (var flow in allFlows)
        {
            Console.WriteLine(flow.StringRepresentation());
        }

        Console.WriteLine("Get all questions of flow 1");
        var allQuestions = _manager.GetAllQuestionsOfFlowById(2);
        foreach (var question in allQuestions)
        {
            Console.WriteLine(question.Text);
        }
        
        
        

    }
}