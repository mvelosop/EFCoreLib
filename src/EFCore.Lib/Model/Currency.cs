using System.ComponentModel.DataAnnotations;

namespace EFCore.Lib.Model
{
    public class Currency
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        public string IsoCode { get; set; }

        [Required]
        [MaxLength(100)] // Default string length
        public string Name { get; set; }

        public byte[] RowVersion { get; set; }

        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; }
    }
}
