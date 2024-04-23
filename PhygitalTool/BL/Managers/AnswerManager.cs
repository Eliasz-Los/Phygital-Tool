using Phygital.DAL;
using Phygital.Domain.Questionsprocess;

namespace Phygital.BL.Managers;

public class AnswerManager : IAnswerManager
{
    private readonly IAnswerRepository _answerRepository;

    public AnswerManager(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public void AddAnswersToFlow(List<Answer> answers)
    {
        foreach (var answer in answers)
        {
            _answerRepository.CreateAnswer(answer);
        }
    }
}