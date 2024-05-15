using Practica.Data.Models;
using System.Data.Entity;

namespace Practica.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("MyHomeConnection") { }

        public static DataContext Create() => new DataContext();

        public DbSet<User> User { get; set; }
        public DbSet<Doc> Doc { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Email)
                .HasMany(u => u.Docs)
                .WithRequired(u => u.User);
        }
    }
}