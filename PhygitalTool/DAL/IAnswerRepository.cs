using Phygital.Domain.Questionsprocess;

namespace Phygital.DAL;

public interface IAnswerRepository
{
    void CreateAnswer(Answer answer);
}