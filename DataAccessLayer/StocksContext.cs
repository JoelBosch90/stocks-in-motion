using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace DataAccessLayer.Models
{
    public class StocksContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StocksAPI> StocksAPIs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "stockpricedb"; 
                builder.UserID = "local";            
                builder.Password = "secret";     
                builder.InitialCatalog = "stockpricedb";

                optionsBuilder.UseNpgsql(builder.ConnectionString);
            }
        }
    }
}