using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using birdcageshopinterface.IServices;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ProductReviews;
using BirdCageShopViewModel.Role;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
	public interface IProductService : IBaseService
	{
		Task<IEnumerable<ProductViewModel>> GetProductsAsync();
		Task<Pagination<ProductViewModel>> GetPageAsync(int pageIndex, int pageSize);
        Task<Pagination<ProductViewModel>> GetByTilePageAsync(string title, int pageIndex, int pageSize);
        Task<Pagination<ProductViewModel>> GetByBirdCageTypePageAsync(int birdCageTypeId, int pageIndex, int pageSize);
		Task<Pagination<ProductViewModel>> GetByCagegoryTypePageAsync(int categoryId, int pageIndex, int pageSize);
        /*Task UpdateProductAsync(int key, UpdateFeature updateFeature);*/
        Task<ProductWithReviewViewModel?> GetByIdAsync(int id);
		Task<Product?> GetProductByIdAsync(int id);
        
        Task<bool> AddToWishlistAsync(int productId);
        Task<bool> MoveProFromWishlistToShoppingCart();
        Task<bool> RemoveProductFromWishlistAsync(int productId);
        Task<IEnumerable<ProductViewModel>> GetProductByCategoryAsync(int categoryId);
		Task<bool> RemoveAsync(Product product);

		//
		Task<ProductWithReviewViewModel?> GetFeedBackByProductId(int productId);

		//
		Task<IEnumerable<ProductViewModel>> GetProductsFromWishlistAsync();
		//
		Task<bool> AddReviewProduct(int productId, AddReviewProductViewModel addReviewProductViewModel);

		Task<IActionResult> CreateProductAsync(CreateProductViewModel requestBody);
		Task<IActionResult> UpdateProductAsync(int id, UpdateProductViewModel requestBody);
	}
}
