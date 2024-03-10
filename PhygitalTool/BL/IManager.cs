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
    Answer GetAnswerById(int id);
    Flow GetFlowById(int id);
    FlowElement GetFlowElementById(int id);
    Image GetImageById(int id);
    Info GetInfoById(int id);
    Question GetQuestionById(int id);
    Text GetTextById(int id);
    Thema GetThemaById(int id);
    Video GetVideoById(int id);

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
    Flow GetAllFlowsIncludingFlowElementById(int id);

    //Filter
    IEnumerable<Question> GetAllQuestionsOfFlowById(int flowId);
    IEnumerable<Flow> GetAllFlowsOfFlowTypeById(int flowTypeId);
    IEnumerable<FlowElement> GetAllFlowElementsOfFlowById(int flowId);
    IEnumerable<Answer> GetAllAnswersOfQuestionById(int questionId);
    IEnumerable<Info> GetAllInfosOfThemaById(int themaId);
    IEnumerable<Question> GetAllQuestionsOfThemaById(int themaId);

    // Delete
    void DeleteFlowElementById(int flowId, int flowElementId);

}