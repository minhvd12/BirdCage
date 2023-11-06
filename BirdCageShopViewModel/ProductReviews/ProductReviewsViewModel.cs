using BirdCageShopViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.ProductReviews
{
	public class ProductReviewsViewModel
	{
        public int Id { get; set; }
        public int? Rating { get; set; }
		public string? ReviewText { get; set; }
		public DateTime? ReviewDate { get; set; }

		//public virtual UserViewModel User { get; set; } = null!;
		public string? LastName { get; set; }

	}
}
	