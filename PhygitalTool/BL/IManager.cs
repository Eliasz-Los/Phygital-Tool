using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Themas;

namespace BL;

public interface IManager
{
    //Questionprocess
    Answer addAnswer(string answerText, Question question);
    Flow addFlow(Flowtype flowtype);
    FlowElement addFlowElement(FlowElement flowElement);
    Image addImage(string imageLink, Question question);
    Info addInfo(string infoText, Flow flow);
    Question addQuestion(string questionText, Flow flow);
    Text addText(string text, Question question);
    Thema addThema(string themaName);
    Video addVideo(string videoLink, Question question);

    //ID
    Answer GetAnswer(int id);
    Flow GetFlow(int id);
    FlowElement GetFlowElement(int id);
    Image GetImage(int id);
    Info GetInfo(int id);
    Question GetQuestion(int id);
    Text GetText(int id);
    Thema GetThema(int id);
    Video GetVideo(int id);

    //ALL
    IEnumerable<Answer> GetAllAnswers();
    IEnumerable<Flow> GetAllFlows();
    IEnumerable<FlowElement> GetAllFlowElements();
    IEnumerable<Image> GetAllImages();
    IEnumerable<Info> GetAllInfos();
    IEnumerable<Question> GetAllQuestions();
    IEnumerable<Text> GetAllTexts();
    IEnumerable<Thema> GetAllThemas();
    IEnumerable<Video> GetAllVideos();
    Flow GetAllFlowsIncludingFlowElement(int id);

    //Filter
    IEnumerable<Question> GetAllQuestionsOfFlow(int flowId);
    IEnumerable<Flow> GetAllFlowsOfFlowType(int flowTypeId);
    IEnumerable<FlowElement> GetAllFlowElementsOfFlow(int flowId);
    IEnumerable<Answer> GetAllAnswersOfQuestion(int questionId);
    IEnumerable<Info> GetAllInfosOfThema(int themaId);
    IEnumerable<Question> GetAllQuestionsOfThema(int themaId);

    // Delete
    void DeleteFlowElement(int flowId, int flowElementId);

}