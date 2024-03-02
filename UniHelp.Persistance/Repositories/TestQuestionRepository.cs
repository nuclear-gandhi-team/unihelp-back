using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class TestQuestionRepository : Repository<TestQuestion>, ITestQuestionRepository
{
    public TestQuestionRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
    }
}