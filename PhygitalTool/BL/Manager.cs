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

    public Answer GetAnswerById(int id)
    {
        return _repository.ReadAnswerById(id);
    }

    public Flow GetFlowById(int id)
    {
        return _repository.ReadFlowById(id);
    }

    public FlowElement GetFlowElementById(int id)
    {
        return _repository.ReadFlowElementById(id);
    }

    public Image GetImageById(int id)
    {
        return _repository.ReadImageById(id);
    }

    public Info GetInfoById(int id)
    {
        return _repository.ReadInfoById(id);
    }

    public Question GetQuestionById(int id)
    {
        return _repository.ReadQuestionById(id);
    }

    public Text GetTextById(int id)
    {
        return _repository.ReadTextById(id);
    }

    public Thema GetThemaById(int id)
    {
        return _repository.ReadThemaById(id);
    }

    public Video GetVideoById(int id)
    {
        return _repository.ReadVideoById(id);
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

    public Flow GetAllFlowsIncludingFlowElementById(int id)
    {
        return _repository.ReadAllFlowsIncludingFlowElementById(id);
    }

    public IEnumerable<Question> GetAllQuestionsOfFlowById(int flowId)
    {
        return _repository.ReadAllQuestionsOfFlowById(flowId);
    }

    public IEnumerable<Flow> GetAllFlowsOfFlowTypeById(int flowTypeId)
    {
        return _repository.ReadAllFlowsOfFlowTypeById(flowTypeId);
    }

    public IEnumerable<FlowElement> GetAllFlowElementsOfFlowById(int flowId)
    {
        return _repository.ReadAllFlowElementsOfFlowById(flowId);
    }

    public IEnumerable<Answer> GetAllAnswersOfQuestionById(int questionId)
    {
        return _repository.ReadAllAnswersOfQuestionById(questionId);
    }

    public IEnumerable<Info> GetAllInfosOfThemaById(int themaId)
    {
        return _repository.ReadAllInfosOfThemaById(themaId);
    }

    public IEnumerable<Question> GetAllQuestionsOfThemaById(int themaId)
    {
        return _repository.ReadAllQuestionsOfThemaById(themaId);
    }

    public void DeleteFlowElementById(int flowId, int flowElementId)
    {
        _repository.DeleteFlowElementById(flowId, flowElementId);
    }

    //TODO write all the adds
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