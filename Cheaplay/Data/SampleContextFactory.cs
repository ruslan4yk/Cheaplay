using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Cheaplay.Data
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<CheaplayContext>
    {
        public CheaplayContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<CheaplayContext>();
            var connString = configuration.GetConnectionString("DevConnection");
            builder.UseSqlServer(connString, b => b.MigrationsAssembly("Cheaplay"));

            return new CheaplayContext(builder.Options);
        }
    }
}
