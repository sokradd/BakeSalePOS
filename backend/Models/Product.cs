using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BakeSale.API.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public decimal Cost { get; set; }
        
        public required string ProductType { get; set; }
        public int StartingQuantity { get; set; }
        public int CurrentQuantity { get; set; }
        [JsonIgnore] public List<OrderLine> OrderLines { get; set; } = new();
    }
}