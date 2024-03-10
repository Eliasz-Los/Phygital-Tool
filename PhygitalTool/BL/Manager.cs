using Phygital.Domain.Questionsprocess;

namespace BL;

public class Manager : IManager
{
    private readonly IRepository _repository;
    public Manager(IRepository repository)
    {
        _repository = repository;
    }

    public Answer getAnswer(int id)
    {
        return _repository.ReadAnswer(id);
    }

    public Flow getFlow(int id)
    {
        return _repository.ReadFlow(id);
    }

public FlowElement getFlowElement(int id)
    {
        return _repository.ReadFlowElement(id);
    }


}