using Microsoft.EntityFrameworkCore;
using EloquentBackend.Data;
using EloquentBackend.Models;
using EloquentBackend.Interfaces.Services;
using DevOne.Security.Cryptography.BCrypt;

namespace EloquentBackend.Services
{
    public class UserService : IUserService
    {
        private readonly ApiDbContext _db;
        public UserService(ApiDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _db.Users
                .Include(u => u.Groups)
                .AsNoTracking()
                .ToListAsync();

            return users;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _db.Users
                .Include(u => u.Groups)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Password = BCryptHelper.HashPassword(user.Password, BCryptHelper.GenerateSalt());
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Password = BCryptHelper.HashPassword(user.Password, BCryptHelper.GenerateSalt());
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return false;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}