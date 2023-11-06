using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ProductImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Order
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public string ProductTitle { get; set; }
        public IEnumerable<ProductImageViewModel> ProductImages { get; set; } 
        //public ProductViewModel Product { get; set; }

    }
}
