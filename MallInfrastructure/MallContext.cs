using MallDomain.entity.mall;
using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure {
    public class MallContext : DbContext {
        public MallContext(DbContextOptions<MallContext> options) : base(options) {
        }

        public DbSet<MallOrderAddress> MallOrderAddressese { get; set; }
        public DbSet<MallShoppingCartItem> MallShoppingCartItems { get; set; }
        public DbSet<MallUser> MallUsers { get; set; }
        public DbSet<MallUserAddress> MallUserAddresses { get; set; }
        public DbSet<MallUserToken> MallUserTokens { get; set; }
        public DbSet<MallAdminUser> AdminUsers { get; set; }
        public DbSet<MallAdminUserToken> MallAdminUserTokens { get; set; }
        public DbSet<MallCarousel> MallCarousels { get; set; }
        public DbSet<MallGoodsCategory> MallGoodsCategories { get; set; }
        public DbSet<MallGoodsInfo> MallGoodsInfos { get; set; }
        public DbSet<MallIndexConfig> MallIndexConfigs { get; set; }
        public DbSet<MallOrder> MallOrders { get; set; }
        public DbSet<MallOrderItem> MallOrderItems { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            /* modelBuilder.ApplyConfiguration(new MallOrderAddressConfig());
             modelBuilder.ApplyConfiguration(new MallShoppingCartItemConfig());
             modelBuilder.ApplyConfiguration(new MallUserAddressConfig());
             modelBuilder.ApplyConfiguration(new MallUserConfig());
             modelBuilder.ApplyConfiguration(new MallUserTokenConfig());*/
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MallContext).Assembly);
        }
    }
}
