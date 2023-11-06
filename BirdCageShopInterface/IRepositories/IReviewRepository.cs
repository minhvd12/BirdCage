using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
    public interface IReviewRepository : IBaseRepository<ProductReview>
    {
        Task<ProductReview> GetReviewByProIAndReviewIddAsync(int productID, int reviewId);
    }
}
