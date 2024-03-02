using Microsoft.EntityFrameworkCore;
using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class AnswerVariantRepository: Repository<AnswerVariant>, IAnswerVariantRepository
{
    public AnswerVariantRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
    }
}