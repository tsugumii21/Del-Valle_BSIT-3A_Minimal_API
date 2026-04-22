using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductAPIDemo.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}