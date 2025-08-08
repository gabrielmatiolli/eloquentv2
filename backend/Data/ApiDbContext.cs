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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Perk> Perks { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento muitos-para-muitos entre User e Group (já feito antes)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users);

            // ✅ NOVO: Relacionamento muitos-para-muitos entre Subscription e Perk
            modelBuilder.Entity<Subscription>()
                .HasMany(s => s.Perks)
                .WithMany(p => p.Subscriptions);

            // Relacionamento um-para-muitos entre User (admin) e Group (já feito antes)
            modelBuilder.Entity<Group>()
                .HasOne(g => g.UserAdmin)
                .WithMany() // Um usuário pode ser admin de vários grupos
                .HasForeignKey(g => g.UserAdminId)
                .OnDelete(DeleteBehavior.Restrict); // Impede que um usuário seja deletado se for admin de um grupo

            // ✅ NOVO: Definir que o Email do usuário deve ser único
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ✅ NOVO: Configurar as relações de Record
            modelBuilder.Entity<Record>()
                .HasOne(r => r.User)
                .WithMany(u => u.Records) // A relação inversa que adicionamos em User.cs
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Se um usuário for deletado, seus registros também são.

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Records) // A relação inversa que adicionamos em Category.cs
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Impede deletar uma categoria se ela estiver em uso.
        }

    }
}