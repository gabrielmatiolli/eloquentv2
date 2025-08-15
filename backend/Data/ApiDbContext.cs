using EloquentBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EloquentBackend.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Perk> Perks { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionPerk> SubscriptionPerks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(u => u.Groups).WithMany(g => g.Users);

            // 1. Configurar a chave primária composta para SubscriptionPerk
            modelBuilder
                .Entity<SubscriptionPerk>()
                .HasKey(sp => new { sp.SubscriptionId, sp.PerkId });

            // 2. Configurar a relação de Subscription para SubscriptionPerk (Um-para-Muitos)
            modelBuilder
                .Entity<SubscriptionPerk>()
                .HasOne(sp => sp.Subscription)
                .WithMany(s => s.SubscriptionPerks)
                .HasForeignKey(sp => sp.SubscriptionId);

            // 3. Configurar a relação de Perk para SubscriptionPerk (Um-para-Muitos)
            modelBuilder
                .Entity<SubscriptionPerk>()
                .HasOne(sp => sp.Perk)
                .WithMany(p => p.SubscriptionPerks)
                .HasForeignKey(sp => sp.PerkId);

            modelBuilder
                .Entity<Group>()
                .HasOne(g => g.UserAdmin)
                .WithMany()
                .HasForeignKey(g => g.UserAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder
                .Entity<Record>()
                .HasOne(r => r.User)
                .WithMany(u => u.Records)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Record>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Records)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
