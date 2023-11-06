using BirdCageShopDbContext.Models;
using birdcageshopinterface.IServices;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.Role;

using BirdCageShopViewModel.ShoppingCart;
using BirdCageShopViewModel.User;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IShoppingCartService : IBaseService
    {

        Task<IEnumerable<ShoppingCartViewModel>> GetShoppingCartsAsync();
        Task<bool> ExistProductByIdAndUserIdAsync(int productId);

        Task<bool> CheckoutAsync(ConfirmOrderAddViewModel confirmOrderAddViewModel);
        Task<ProductViewModel> ExistProductAsync(int productId);
        Task<ShoppingCartViewModel?> GetShoppingCartByIdAsync();
        Task<bool> RemoveFromCartAsync(int productId);
        Task<bool> CreateOrUpdateAsync(int productId, int count);

        //
        Task<ValidationResult> ValidateConfirmOrderAddAsync(ConfirmOrderAddViewModel vm);
    }
}
