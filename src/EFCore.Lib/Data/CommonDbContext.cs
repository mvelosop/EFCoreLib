using EFCore.Lib.Base;
using EFCore.Lib.Config;
using EFCore.Lib.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Lib.Data
{
    public class CommonDbContext : DbContext
    {
        // Must not be null or empty for running initial create migration
        private string _connectionString = "ConnectionString";

        // Default constructor for initial create migration
        public CommonDbContext()
        {
        }

        // Normal use constructor
        public CommonDbContext(ConnectionStrings connectionStrings)
        {
            _connectionString = connectionStrings.DefaultConnection;
        }

        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddConfiguration(new CurrencyConfiguration());
        }
    }
}