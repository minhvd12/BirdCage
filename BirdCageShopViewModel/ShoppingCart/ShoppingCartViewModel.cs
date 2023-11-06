using BirdCageShopViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCageShopDomain;
using BirdCageShopDomain.Models;
using BirdCageShopDbContext.Models;
using BirdCageShopViewModel.Order;

namespace BirdCageShopViewModel.ShoppingCart
{
	public class ShoppingCartViewModel
	{
        public int Id { get; set; }
        public int Count { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        //public decimal pricePerUnit { get; set; }   //matching price afterdiscont in products
        //public virtual User User { get; set; } = null!;
        public virtual ProductViewModel ProductViewModel { get; set; } = null!;

        //public decimal Total { get; set; }

        //public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

        //public OrderWithDetailViewModel Order { get; set; }


    }
}
