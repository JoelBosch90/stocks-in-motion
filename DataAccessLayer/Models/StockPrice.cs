using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class StockPrice
    {
        public long Id { get; set; }

        public DateTime Added { get; set; }

        public DateTime Moment { get; set; }

        public string Currency { get; set; }

        public int? Open { get; set; }

        public int? High { get; set; }

        public int? Low { get; set; }

        public int Close { get; set; }

        public long Volume { get; set; }

        [ForeignKey("DataSource")]
        public long DataSourceId { get; set; }

        [ForeignKey("Stock")]
        public long StockId { get; set; }
    }
}
