
using Mall.Repository.Models;
using Microsoft.EntityFrameworkCore;


namespace Mall.Repository
{
    public class MallContext : DbContext
    {
        public MallContext(DbContextOptions<MallContext> options) : base(options)
        {
        }

        public DbSet<OrderAddress> OrderAddressese { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AdminUserToken> AdminUserTokens { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<GoodsCategory> GoodsCategories { get; set; }
        public DbSet<GoodsInfo> GoodsInfos { get; set; }
        public DbSet<IndexConfig> IndexConfigs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
