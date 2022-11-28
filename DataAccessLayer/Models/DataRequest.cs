using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class DataRequest
    {
        public long Id { get; set; }

        public DateTime Added { get; set; }

        public string RequestString { get; set; }

        public int ResponseCode { get; set; }

        [ForeignKey("DataSource")]
        public long DataSourceId { get; set; }

        [ForeignKey("Stock")]
        public long StockId { get; set; }
    }
}
