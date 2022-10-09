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
            if (!optionsBuilder.IsConfigured)
            {
                string host = "stockpricedb";
                int port = 80;
                string userID = "local";
                string password = "secret";
                string database = "stockpricedb";

                optionsBuilder.UseNpgsql($"Host={host};Port={port};Database={database};User ID={userID};Password={password};");
            }
        }
    }
}