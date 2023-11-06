using BirdCageShopDbContext.Models;
using birdcageshopinterface.IServices;
using BirdCageShopViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IProductReviewService : IBaseService
    {
        Task<ProductReview?> GetProductReviewByProductIDAndReviewIdByIdAsync(int proId, int reviewId);

        Task<bool> DeleteReviewProduct(ProductReview reviewOfProduct);
    }
}
