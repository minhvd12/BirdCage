using BirdCageShopViewModel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.ProductSpecifications
{
	public class ProductSpecificationsViewModel
	{
        public int Id { get; set; }
        public string SpecificationName { get; set; } = null!;
		public string SpecificationValue { get; set; } = null!;
		public decimal? Price { get; set; }

        public SpecificationViewModel Specification { get; set; }
    }
}
