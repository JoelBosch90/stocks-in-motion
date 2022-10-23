using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Models
{
    public class StocksContext : DbContext
    {
        // https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli
        #region DbSets
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<DataSource> DataSources { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string host = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
            int port = Int32.Parse(Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "8006");
            string? database = Environment.GetEnvironmentVariable("DATABASE_NAME");
            string? userID = Environment.GetEnvironmentVariable("DATABASE_USERNAME");
            string? password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql($"Host={host};Port={port};Database={database};User ID={userID};Password={password}");
            }
        }
    }
}