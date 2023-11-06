using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.ProductImage
{
	public class ProductImageViewModel
	{
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
		public bool IsMainImage { get; set; }
	}
}
