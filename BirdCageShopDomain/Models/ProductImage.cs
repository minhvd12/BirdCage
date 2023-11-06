using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ProductId { get; set; }
        public bool IsMainImage { get; set; }

        public virtual Product? Product { get; set; }
    }
}
