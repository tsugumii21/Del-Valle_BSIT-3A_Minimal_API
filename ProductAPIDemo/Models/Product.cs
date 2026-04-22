using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductAPIDemo.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Range(1, 1000000)]
        public decimal Price { get; set; }

        [Range(0, 10000)]
        public int Stock { get; set; }

        public int? CategoryID { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        public int? SupplierID { get; set; }

        [JsonIgnore]
        public Supplier? Supplier { get; set; }
    }
}