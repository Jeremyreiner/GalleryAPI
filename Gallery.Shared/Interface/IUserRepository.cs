using System.Linq.Expressions;
using Gallery.Shared.Entities;

namespace Gallery.Shared.Interface;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<User?> GetUserByName(Expression<Func<User, bool>> predicate);
}