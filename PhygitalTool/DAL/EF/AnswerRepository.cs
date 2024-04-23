using Phygital.Domain.Questionsprocess;

namespace Phygital.DAL.EF;

public class AnswerRepository : IAnswerRepository
{
    private readonly PhygitalDbContext _dbContext;

    public AnswerRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void CreateAnswer(Answer answer)
    {
        _dbContext.Answers.Add(answer);
    }
}