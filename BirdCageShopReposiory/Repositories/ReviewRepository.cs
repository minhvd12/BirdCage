using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class ReviewRepository : BaseRepository<ProductReview>, IReviewRepository
    {
        public ReviewRepository(BirdCageShopContext context) : base(context)
        {
        }

        public async Task<ProductReview> GetReviewByProIAndReviewIddAsync(int productID, int reviewId)
        {
            return await _context.Set<ProductReview>()
               .FirstOrDefaultAsync(x => x.ProductId == productID && x.Id == reviewId && x.IsDelete == false);
        }
    }
    }
