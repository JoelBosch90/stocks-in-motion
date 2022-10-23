using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Stock
    {
        public long Id { get; set; }

        public DateTime Added { get; set; }

        [StringLength(255)]
        public string? Name { get; set; }

        [StringLength(5)]
        public string Symbol { get; set; }

        public string Currency { get; set; }

        public int Open { get; set; }

        public int High { get; set; }

        public int Low { get; set; }

        public int Close { get; set; }

        public DataSource Source { get; set; }
    }
}
