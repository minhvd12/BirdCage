using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class WishlistRepository : BaseRepository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(BirdCageShopContext context) : base(context)
        {
        }

        public async Task<Wishlist?> GetWishlistByCustomerIdAndProductIdAsync(string customerId, int productId)
        {
            return await _context.Set<Wishlist>()
                 .AsNoTracking()
                 //.Include(w => w.WishlistItems)
                 .FirstOrDefaultAsync(w => w.ApplicationUserId == customerId && w.ProductId == productId);
        }

        public async Task<IEnumerable<Wishlist>?> GetWishlistByCustomerIdAsync(string customerId)
        {
            return await _context.Set<Wishlist>()
                .AsNoTracking()
                //.Include(w => w.WishlistItems)
                .Where(w => w.ApplicationUserId == customerId).ToListAsync();
        }
    }
}
