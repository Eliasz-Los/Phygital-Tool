using Phygital.DAL;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace BL;

public class FlowManager : IFlowManager
{
    private readonly IFlowRepository _flowRepository;
    public FlowManager(IFlowRepository flowRepository)
    {
        _flowRepository = flowRepository;
    }


    public IEnumerable<Flow> GetAllFlows()
    {
        return _flowRepository.ReadAllFlows();
    }

    public Flow GetFlowById(long id)
    {
        return _flowRepository.ReadFlowById(id);
    }

    public IEnumerable<SingleChoiceQuestion> GetSingleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _flowRepository.ReadSingleChoiceQuestionsWithOptionsOfFlowById(flowId);
    }

    public IEnumerable<MultipleChoice> GetMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _flowRepository.ReadMultipleChoiceQuestionsWithOptionsOfFlowById(flowId);
    }

    public IEnumerable<RangeQuestion> GetRangeQuestionsWithOptionsOfFlowById(long flowId)
    {
    return _flowRepository.ReadRangeQuestionsWithOptionsOfFlowById(flowId);
    }

    public IEnumerable<OpenQuestion> GetOpenQuestionsWithAnswerOfFlowById(long flowId)
    {
        return _flowRepository.ReadOpenQuestionsWithAnswerOfFlowById(flowId);
    }

    public IEnumerable<Theme> GetSubThemasFlow(long flowId)
    {
        return _flowRepository.ReadSubThemasFlow(flowId);
    }

    public IEnumerable<Theme> GetAllSubThemas()
    {
        return _flowRepository.ReadAllSubThemas();
    }

    public Option GetOptionByText(string optionText)
    {
        return _flowRepository.ReadOptionByText(optionText);
    }

    public void AddAnswersToFlow(Flow flow, List<ICollection<Option>> chosenOptionsList, List<string> chosenAnswers, Theme subtheme)
    {
        List<Answer> answers = new List<Answer>();

        for (int i = 0; i < chosenOptionsList.Count; i++)
        {
            Answer answer = new Answer
            {
                Flow = flow,
                SubTheme = subtheme,
                ChosenOptions = chosenOptionsList[i],
                ChosenAnswer = chosenAnswers[i]
            };
            _flowRepository.CreateAnswer(answer);
        }
    }
}