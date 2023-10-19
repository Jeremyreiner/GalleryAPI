using Gallery.DataBase.Infrastructure.MySql;
using Gallery.Shared.Entities;
using System.Linq.Expressions;
using Gallery.Shared.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gallery.DataBase.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly ApplicationDbContext _DbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task CreateUser(User user)
        {
            await _DbContext.Users.AddAsync(user);

            await _DbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByName(Expression<Func<User, bool>> predicate) =>
            await _DbContext.Users.FirstOrDefaultAsync(predicate);

    }
}
