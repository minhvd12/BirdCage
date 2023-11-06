using BirdCageShopViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.ShoppingCart
{
    public class ShoppingCartWithOrderVM
    {
        public IEnumerable<ShoppingCartViewModel> ShoppingCartList { get; set; }
        public OrderWithDetailViewModel Order { get; set; }
    }
}
