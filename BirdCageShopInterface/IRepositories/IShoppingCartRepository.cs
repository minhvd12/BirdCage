using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
	public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>
	{
		Task<IEnumerable<ShoppingCart>> GetShoppingCartsAsync(string customerId);
		Task DeleteShoppingCartsByUserIdAsync(string customerId);
		Task<ShoppingCart> GetCartItemByUserIdAndProDIdAsync(string customerId, int prodID);


	}
}
