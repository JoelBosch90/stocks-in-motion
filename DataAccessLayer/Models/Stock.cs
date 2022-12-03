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
    }
}
