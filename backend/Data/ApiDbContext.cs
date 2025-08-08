using Microsoft.EntityFrameworkCore;
using EloquentBackend.Models;

namespace EloquentBackend.Data
{
    public class ApiDbContext : DbContext
    {
        // O construtor recebe as opções de configuração do banco de dados
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        // Mapeia o modelo 'User' para uma tabela 'Users' no banco de dados
        public DbSet<User> Users { get; set; }

    }
}