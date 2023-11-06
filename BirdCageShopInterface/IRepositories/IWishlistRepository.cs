using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
    public interface IWishlistRepository : IBaseRepository<Wishlist>
    {
        Task<IEnumerable<Wishlist>?> GetWishlistByCustomerIdAsync(string customerId);
        Task<Wishlist?> GetWishlistByCustomerIdAndProductIdAsync(string customerId, int productId);
    }
}
