using System;
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
        public string Ticker { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
        public StocksAPI Source { get; set; }
        public DateTime Moment { get; set; }
    }
}
