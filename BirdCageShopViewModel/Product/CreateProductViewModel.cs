using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Product
{
    public class CreateProductViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int BirdCageTypeId { get; set; }
        [Required]
        [Range(150000, 9000000, ErrorMessage = "Price invalid.")]
        public decimal Price { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        [Range(0, 300, ErrorMessage = "Quantity invalid.")]
        public int QuantityInStock { get; set; }
        [Range(0, 100, ErrorMessage = "PercentDiscount invalid.")]
        public decimal? PercentDiscount { get; set; }

        public ICollection<int> ProductSpecifications { get; set; } 

        public ICollection<int?> ProductFeature { get; set; }
        [Required]
        public List<IFormFile> files { get; set; } = new List<IFormFile>();

    }
}
