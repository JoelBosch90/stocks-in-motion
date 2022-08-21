using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Models
{
    public class StocksContext : DbContext
    {
        // https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli
        #region DbSets
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StocksAPI> StocksAPIs { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string host = "stockpricedb";
                string userID = "123test";
                string password = "test123";
                string database = "stocksinmotion";

                optionsBuilder.UseNpgsql($"host={host};database={database};user id={userID};password={password};");
            }
        }

        
    }
}