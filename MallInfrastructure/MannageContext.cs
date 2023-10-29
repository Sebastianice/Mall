using System.Reflection;
using MallDomain.entity.mannage;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure {
    public class MannageContext : DbContext {
        public DbSet<MallAdminUser> AdminUsers { get; set; }
        public DbSet<MallAdminUserToken> MallAdminUserTokens { get; set; }
        public DbSet<MallCarousel> MallCarousels { get; set; }
        public DbSet<MallGoodsCategory> MallGoodsCategories { get; set; }
        public DbSet<MallGoodsInfo> MallGoodsInfos { get; set; }
        public DbSet<MallIndexConfig> MallIndexConfigs { get;set; }
        public DbSet<MallOrder> MallOrders { get; set; }
        public DbSet<MallOrderItem> MallOrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public MannageContext(DbContextOptions options) : base(options) {
        }
    }
}
