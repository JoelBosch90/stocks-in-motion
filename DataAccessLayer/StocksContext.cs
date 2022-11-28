using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Models
{
    public class StocksContext : DbContext
    {
        // https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli
        #region DbSets
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<DataRequest> DataRequests { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string host = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
            int port = Int32.Parse(Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "8006");
            string? database = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "stockpricedb";
            string? userID = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? "local";
            string? password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "secret";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql($"Host={host};Port={port};Database={database};User ID={userID};Password={password};Include Error Detail=True");
            }
        }
    }
}