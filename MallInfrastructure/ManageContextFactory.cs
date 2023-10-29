using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace MallInfrastructure {
    internal class ManageContextFactory : IDesignTimeDbContextFactory<MannageContext> {
        public ILoggerFactory loggerFactory = new LoggerFactory();
        MannageContext IDesignTimeDbContextFactory<MannageContext>.CreateDbContext(string[] args) {
            var opt = new DbContextOptionsBuilder<MallContext>();
            opt.UseLoggerFactory(loggerFactory);
            string con = "server=192.168.0.6;user=root;password=root;database=ef";
            var version = new MySqlServerVersion(new Version(8, 1, 0));
            opt.UseMySql(con, version);
            return new MannageContext(opt.Options);
        }
    }


}
