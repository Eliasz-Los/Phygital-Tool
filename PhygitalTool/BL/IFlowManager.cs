using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace BL;

public interface IFlowManager
{

    IEnumerable<Flow> GetAllFlows();
    Flow GetFlowById(long id);
    IEnumerable<SingleChoiceQuestion> GetSingleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<MultipleChoice> GetMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<RangeQuestion> GetRangeQuestionsWithOptionsOfFlowById(long flowId);
    IEnumerable<OpenQuestion> GetOpenQuestionsWithAnswerOfFlowById(long flowId);
    IEnumerable<Theme> GetSubThemasFlow(long flowId);
    Option GetOptionByText(string optionText);
    Answer AddAnswerToFlow(Flow flow, ICollection<Option> chosenOptions, string chosenAnswer,Theme subtheme);
    
}