using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MallInfrastructure
{
    internal class MallContextFactory : IDesignTimeDbContextFactory<MallContext>
    {
        public MallContext CreateDbContext(string[] args)
        {
            var opt = new DbContextOptionsBuilder<MallContext>();
            string con = "server=192.168.0.6;user=root;password=root;database=ef";
            var version = new MySqlServerVersion(new Version(8, 1, 0));
            opt.UseMySql(con, version);
            return new MallContext(opt.Options);
        }
    }
}
