using BirdCageShopDomain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdCageShopDbContext.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
            ProductReviews = new HashSet<ProductReview>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
		public string? Description { get; set; }

        public int CategoryId { get; set; }
        public int? BirdCageTypeId { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public DateTime? ModifieldAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public bool isDelete { get; set; }
		public decimal Price { get; set; }
		public string SKU { get; set; }
		public int QuantityInStock { get; set; }
        public string? EditedBy { get; set; }
        public decimal? PercentDiscount { get; set; }
		public decimal? PriceAfterDiscount { get; set; }


		public Category Category { get; set; }
        public BirdCageType BirdCageType { get; set; }
        //
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        //public virtual ICollection<WishlistItem> WishlistItems { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        //public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; }
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }


    }
}
