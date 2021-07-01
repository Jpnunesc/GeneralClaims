using Domain.Entity;
using Domain.Entitys;
using Infra.Mappings;
using Microsoft.EntityFrameworkCore;


namespace Infra.Context
{
    public class GeneralClaimsContext : DbContext
    {
        public GeneralClaimsContext(DbContextOptions<GeneralClaimsContext> options)
      : base(options)
        {
        }
        public DbSet<FavoritosEntity> Favoritos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new FavoritoMap(modelBuilder.Entity<FavoritosEntity>());
        }
    }
}
