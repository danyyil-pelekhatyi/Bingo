using BingoBongo.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BingoBongo.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository, IRepository<User>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
