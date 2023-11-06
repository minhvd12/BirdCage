using BirdCageShopViewModel.Category;
using BirdCageShopViewModel.Feature;

using BirdCageShopViewModel.ProductFeature;
using BirdCageShopViewModel.ProductImage;
using BirdCageShopViewModel.ProductReviews;
using BirdCageShopViewModel.ProductSpecifications;
using BirdCageShopViewModel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Product
{
	public class ProductViewModel
	{
        public int Id { get; set; }
        public string? Title { get; set; }
		public string? Description { get; set; }
		public int CategoryId { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? ModifieldAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		//public bool isDelete { get; set; }
		public decimal Price { get; set; }
		public string SKU { get; set; }
		public int QuantityInStock { get; set; }
		public string? EditedBy { get; set; }
		public decimal? PercentDiscount { get; set; }
		public decimal PriceAfterDiscount { get; set; }

		public CategoryViewModel Category { get; set; }
		//public IEnumerable<ProductFeatureViewModel> ProductFeatures { get; set; }
		//public IEnumerable<ProductSpecificationsViewModel> ProductSpecifications { get; set; }

		public IEnumerable<SpecificationViewModel> Specifications { get; set; }
        public IEnumerable<FeatureViewModel> Features { get; set; }
        
        public IEnumerable<ProductImageViewModel> ProductImages { get; set; }
	}
}
