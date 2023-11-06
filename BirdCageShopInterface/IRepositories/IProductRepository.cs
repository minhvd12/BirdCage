using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
	public interface IProductRepository : IBaseRepository<Product>
	{
		Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
		Task<Product> GetProductWithReviewByProIdAsync(int productID);
		Task<Product?> GetProductByProductIdAndCustomerIdAsync(string customerId, int productId);

		Task<Product> GetProductIncludeImage(int productID);
		Task<IEnumerable<Product>> GetProductsFromWishlistAsync(string customerId);




	}
}
