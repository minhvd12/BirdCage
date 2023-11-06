using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        public int? Rating { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string? DeletedAt { get; set; }
        public string? CreateAt { get; set; }
        public bool IsDelete { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
