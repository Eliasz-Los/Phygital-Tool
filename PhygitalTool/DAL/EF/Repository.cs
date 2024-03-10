using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Themas;

namespace Phygital.DAL.EF;

public class Repository : IRepository
{
    public Answer ReadAnswerById(int id)
    {
        throw new NotImplementedException();
    }

    public Flow ReadFlowById(int id)
    {
        throw new NotImplementedException();
    }

    public FlowElement ReadFlowElementById(int id)
    {
        throw new NotImplementedException();
    }

    public Image ReadImageById(int id)
    {
        throw new NotImplementedException();
    }

    public Info ReadInfoById(int id)
    {
        throw new NotImplementedException();
    }

    public Question ReadQuestionById(int id)
    {
        throw new NotImplementedException();
    }

    public Text ReadTextById(int id)
    {
        throw new NotImplementedException();
    }

    public Thema ReadThemaById(int id)
    {
        throw new NotImplementedException();
    }

    public Video ReadVideoById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Answer> ReadAllAnswers()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Flow> ReadAllFlows()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<FlowElement> ReadAllFlowElements()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Image> ReadAllImages()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Info> ReadAllInfos()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Question> ReadAllQuestions()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Text> ReadAllTexts()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Thema> ReadAllThemas()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Video> ReadAllVideos()
    {
        throw new NotImplementedException();
    }

    public Flow ReadAllFlowsIncludingFlowElementById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Question> ReadAllQuestionsOfFlowById(int flowId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Flow> ReadAllFlowsOfFlowTypeById(int flowTypeId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<FlowElement> ReadAllFlowElementsOfFlowById(int flowId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Answer> ReadAllAnswersOfQuestionById(int questionId)
    {
        throw new NotImplementedException();
    }

    public void DeleteFlowElementById(int flowId, int flowElementId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Question> ReadAllQuestionsOfThemaById(int themaId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Info> ReadAllInfosOfThemaById(int themaId)
    {
        throw new NotImplementedException();
    }
}