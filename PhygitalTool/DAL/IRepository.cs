using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Themas;

namespace Phygital.DAL;

public interface IRepository
{
    Answer ReadAnswerById(int id);
    Flow ReadFlowById(int id);
    FlowElement ReadFlowElementById(int id);
    Image ReadImageById(int id);
    Info ReadInfoById(int id);
    Question ReadQuestionById(int id);
    Text ReadTextById(int id);
    Thema ReadThemaById(int id);
    Video ReadVideoById(int id);
    IEnumerable<Answer> ReadAllAnswers();
    IEnumerable<Flow> ReadAllFlows();
    IEnumerable<FlowElement> ReadAllFlowElements();
    IEnumerable<Image> ReadAllImages();
    IEnumerable<Info> ReadAllInfos();
    IEnumerable<Question> ReadAllQuestions();
    IEnumerable<Text> ReadAllTexts();
    IEnumerable<Thema> ReadAllThemas();
    IEnumerable<Video> ReadAllVideos();
    Flow ReadAllFlowsIncludingFlowElementById(int id);
    IEnumerable<Question> ReadAllQuestionsOfFlowById(int flowId);
    IEnumerable<Flow> ReadAllFlowsOfFlowTypeById(int flowTypeId);
    IEnumerable<FlowElement> ReadAllFlowElementsOfFlowById(int flowId);
    IEnumerable<Answer> ReadAllAnswersOfQuestionById(int questionId);
    void DeleteFlowElementById(int flowId, int flowElementId);
    IEnumerable<Question> ReadAllQuestionsOfThemaById(int themaId);
    IEnumerable<Info> ReadAllInfosOfThemaById(int themaId);
}