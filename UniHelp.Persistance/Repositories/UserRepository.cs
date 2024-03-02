using UniHelp.Domain.Entities;
using UniHelp.Persistance.Context;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Persistance.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(UniDataContext dbContext) 
        : base(dbContext)
    {
    }
}