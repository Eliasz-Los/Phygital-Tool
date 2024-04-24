using Phygital.Domain.Questionsprocess;

namespace Phygital.BL;

public interface IAnswerManager
{
    void AddAnswersToFlow(List<Answer> answers);
}