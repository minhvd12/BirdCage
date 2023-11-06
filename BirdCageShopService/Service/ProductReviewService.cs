using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class ProductReviewService : BaseService, IProductReviewService
    {
        public ProductReviewService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
        }

        public async Task<bool> DeleteReviewProduct(ProductReview reviewOfProduct)
        {
            reviewOfProduct.IsDelete = true;
            _unitOfWork.ReviewRepository.Update(reviewOfProduct);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductReview?> GetProductReviewByProductIDAndReviewIdByIdAsync(int proId, int reviewId)
        {
            return await _unitOfWork.ReviewRepository.GetReviewByProIAndReviewIddAsync(proId, reviewId);
        }
    }
    }

