using System.Reflection;
using MallDomain.entity.mall;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure {
    public class MallContext : DbContext {
        public MallContext(DbContextOptions options) : base(options) {
        }

        public DbSet<MallOrderAddress> MallOrderAddressese { get; set; }
        public DbSet<MallShoppingCartItem> MallShoppingCartItems { get; set; }
        public DbSet<MallUser> MallUserAddresse { get; set; }
        public DbSet<MallUserAddress> MallUserAddresses { get; set; }
        public DbSet<MallUserToken> MallUserTokens { get; set; }
      

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
