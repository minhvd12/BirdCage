using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.ProductImage
{
    public class CreateProductImageViewModel
    {
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public int? ProductId { get; set; }
        [Required]
        public bool IsMainImage { get; set; }
    }
}
