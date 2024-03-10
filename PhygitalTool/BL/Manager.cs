using Phygital.DAL;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Themas;

namespace BL;

public class Manager : IManager
{
    private readonly IRepository _repository;
    public Manager(IRepository repository)
    {
        _repository = repository;
    }

    public Answer GetAnswer(int id)
    {
        return _repository.ReadAnswer(id);
    }

    public Flow GetFlow(int id)
    {
        return _repository.ReadFlow(id);
    }

    public FlowElement GetFlowElement(int id)
    {
        return _repository.ReadFlowElement(id);
    }

    public Image GetImage(int id)
    {
        return _repository.ReadImage(id);
    }

    public Info GetInfo(int id)
    {
        return _repository.ReadInfo(id);
    }

    public Question GetQuestion(int id)
    {
        return _repository.ReadQuestion(id);
    }

    public Text GetText(int id)
    {
        return _repository.ReadText(id);
    }

    public Thema GetThema(int id)
    {
        return _repository.ReadThema(id);
    }

    public Video GetVideo(int id)
    {
        return _repository.ReadVideo(id);
    }


    //All
    public IEnumerable<Answer> GetAllAnswers()
    {
        return _repository.ReadAllAnswers();
    }

    public IEnumerable<Flow> GetAllFlows()
    {
        return _repository.ReadAllFlows();
    }

    public IEnumerable<FlowElement> GetAllFlowElements()
    {
        return _repository.ReadAllFlowElements();
    }

    public IEnumerable<Image> GetAllImages()
    {
        return _repository.ReadAllImages();
    }

    public IEnumerable<Info> GetAllInfos()
    {
        return _repository.ReadAllInfos();
    }

    public IEnumerable<Question> GetAllQuestions()
    {
        return _repository.ReadAllQuestions();
    }

    public IEnumerable<Text> GetAllTexts()
    {
        return _repository.ReadAllTexts();
    }

    public IEnumerable<Thema> GetAllThemas()
    {
        return _repository.ReadAllThemas();
    }

    public IEnumerable<Video> GetAllVideos()
    {
        return _repository.ReadAllVideos();
    }

    public Flow GetAllFlowsIncludingFlowElement(int id)
    {
        return _repository.ReadAllFlowsIncludingFlowElement(id);
    }

    public IEnumerable<Question> GetAllQuestionsOfFlow(int flowId)
    {
        return _repository.ReadAllQuestionsOfFlow(flowId);
    }

    public IEnumerable<Flow> GetAllFlowsOfFlowType(int flowTypeId)
    {
        return _repository.ReadAllFlowsOfFlowType(flowTypeId);
    }

    public IEnumerable<FlowElement> GetAllFlowElementsOfFlow(int flowId)
    {
        return _repository.ReadAllFlowElementsOfFlow(flowId);
    }

    public IEnumerable<Answer> GetAllAnswersOfQuestion(int questionId)
    {
        return _repository.ReadAllAnswersOfQuestion(questionId);
    }

    public IEnumerable<Info> GetAllInfosOfThema(int themaId)
    {
        return _repository.ReadAllInfosOfThema(themaId);
    }

    public IEnumerable<Question> GetAllQuestionsOfThema(int themaId)
    {
        return _repository.ReadAllQuestionsOfThema(themaId);
    }

    public void DeleteFlowElement(int flowId, int flowElementId)
    {
        _repository.DeleteFlowElement(flowId, flowElementId);
    }


    //Add
    public Answer addAnswer(string answerText, Question question)
    {
        throw new NotImplementedException();
    }

    public Flow addFlow(Flowtype flowtype)
    {
        throw new NotImplementedException();
    }

    public FlowElement addFlowElement(FlowElement flowElement)
    {
        throw new NotImplementedException();
    }

    public Image addImage(string imageLink, Question question)
    {
        throw new NotImplementedException();
    }

    public Info addInfo(string infoText, Flow flow)
    {
        throw new NotImplementedException();
    }

    public Question addQuestion(string questionText, Flow flow)
    {
        throw new NotImplementedException();
    }

    public Text addText(string text, Question question)
    {
        throw new NotImplementedException();
    }

    public Thema addThema(string themaName)
    {
        throw new NotImplementedException();
    }

    public Video addVideo(string videoLink, Question question)
    {
        throw new NotImplementedException();
    }


}