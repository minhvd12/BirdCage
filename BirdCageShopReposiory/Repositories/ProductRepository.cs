using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using BirdCageShopUtils.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        public ProductRepository(BirdCageShopContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Set<Product>()
                .AsNoTracking()
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductSpecifications)
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Where(x => !x.isDelete)
                .ToListAsync();
        }

        public virtual async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Set<Product>()
                .AsNoTracking()
                 .Where(x => x.Id == id)
                 .Include(p => p.ProductSpecifications)
            .ThenInclude(ps => ps.Specification)
                .Include(p => p.ProductFeatures)
            .ThenInclude(ps => ps.Feature)
             .Include(p => p.ProductReviews.Where(pr => pr.IsDelete == false))
                .ThenInclude(pr => pr.ApplicationUser)
            .Include(p => p.ProductImages).FirstOrDefaultAsync();

        }
        public override async Task<Pagination<Product>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var totalCount = await _context.Set<Product>().CountAsync();
            var items = await _context.Set<Product>()
                .AsNoTracking()

                   .Include(p => p.ProductSpecifications)
            .ThenInclude(ps => ps.Specification)
                .Include(p => p.ProductFeatures)
            .ThenInclude(ps => ps.Feature)
            .Include(p => p.ProductImages)

                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<Product>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Set<Product>()
                   //.AsNoTracking()
                   //.Include(x => x.Category)
                   .AsNoTracking()
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductSpecifications)
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                   //.Include(x => x.ProductWishlist)
                   .Where(x => !x.isDelete && x.CategoryId == categoryId)
                   .ToListAsync();
        }


        public virtual async Task<Pagination<Product>> GetAllByConditionAsync(Expression<Func<Product, bool>> filters, int pageIndex, int pageSize)
        {
            var totalCount = await _context.Set<Product>().CountAsync();
            var items = await _context.Set<Product>()
                .AsNoTracking()
                .Where(filters)
                   .Include(p => p.ProductSpecifications)
            .ThenInclude(ps => ps.Specification)
                .Include(p => p.ProductFeatures)
            .ThenInclude(ps => ps.Feature)
            .Include(p => p.ProductImages)

                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<Product>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsFromWishlistAsync(string customerId)
        {
            return await _context.Set<Product>()
              .AsNoTracking()
              .Include(p => p.Category)

              .Include(p => p.ProductImages)
              .Where(x => !x.isDelete &&
              x.Wishlists.Any(pw => pw.ApplicationUserId == customerId))
              .ToListAsync();
        }

        public async Task<Product> GetProductWithReviewByProIdAsync(int productID)
        {
            return await _context.Set<Product>()
                .AsNoTracking()
                .Include(p => p.ProductReviews)
                .ThenInclude(pr => pr.ApplicationUser)
                .FirstOrDefaultAsync(x => !x.isDelete);
        }

        public async Task<Product> GetProductIncludeImage(int productID)
        {
            return await _context.Set<Product>()
                .AsNoTracking()
                .Include(p => p.ProductImages)
                //.ThenInclude(pr => pr.ApplicationUser)
                .FirstOrDefaultAsync(x => !x.isDelete);
        }

        public async Task<Product?> GetProductByProductIdAndCustomerIdAsync(string customerId, int productId)
        {
            return await _context.Set<Product>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => !x.isDelete && x.Wishlists.Any(wl => wl.ProductId == productId && wl.ApplicationUserId == customerId));
        }
    }
}
