using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class DataSource
    {
        public long Id { get; set; }

        public DateTime Added { get; set; }

        [StringLength(255)]
        public string? Name { get; set; }

        [StringLength(2048)]
        public string Url { get; set; }
    }
}