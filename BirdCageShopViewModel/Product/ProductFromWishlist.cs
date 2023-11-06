using BirdCageShopViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Product
{
	public class ProductFromWishlist
	{
		public string? Title { get; set; }
		public CategoryViewModel Category { get; set; }

		public decimal? PriceAfterDiscount { get; set; }

		public string? ImageUrl { get; set; }  // this is main image of product
	}
}
