using Microsoft.EntityFrameworkCore; // Necessário para os métodos Include e ToListAsync
using System.Collections.Generic;
using System.Threading.Tasks;
using EloquentBackend.Data;
using EloquentBackend.Models;
using EloquentBackend.Interfaces.Services;

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

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _db.Users
                .Include(u => u.Groups)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
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