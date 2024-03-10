using BL;

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
        _manager.GetAllFlows();
        _manager.GetAllThemas();

    }
}