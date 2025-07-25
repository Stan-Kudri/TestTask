using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Core.DBContext;
using TestTask.Core.Exeption;

namespace TestTask.Core.Models.Users
{
    public class UserService(AppDbContext dbContext, IUserValidator userValidator, IPasswordHasher passwordHasher)
    {
        public async Task AddAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            BusinessLogicException.ThrowIfNull(username);
            BusinessLogicException.ThrowIfNull(password);

            if (!userValidator.ValidateUsername(username, out var messageValidUsername))
            {
                throw new BusinessLogicException(messageValidUsername);
            }

            if (!userValidator.ValidatePassword(password, out var messageValidPass))
            {
                throw new BusinessLogicException(messageValidPass);
            }

            if (dbContext.Users.Any(e => e.Username == username))
            {
                throw new BusinessLogicException($"This username {username} exists.");
            }

            var passwordHash = Hash(password);
            var user = new User(username, passwordHash);

            await dbContext.Users.AddAsync(user, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public bool IsFreeUsername(string username)
            => dbContext.Users.FirstOrDefault(e => e.Username == username) == null;

        public async Task<bool> IsDataVerifyUser(string username, string password, CancellationToken cancellationToken = default)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(e => e.Username == username, cancellationToken);
            return user != null && Verify(password, user.PasswordHash);
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public User? GetUser(string username, string password)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = dbContext.Users.FirstOrDefault(e => e.Username == username);

            return user == null && !Verify(password, user.PasswordHash)
                    ? null
                    : new User(username, user.PasswordHash);
        }

        public IQueryable<User> GetQueryableAll() => dbContext.Users.Select(e => e);

        private string Hash(string password) => passwordHasher.Hash(password);

        private bool Verify(string password, string hash) => passwordHasher.Verify(password, hash);
    }
}
