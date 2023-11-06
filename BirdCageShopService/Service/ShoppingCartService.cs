using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ShoppingCart;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BirdCageShopService.Service
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        private readonly IConfirmOrderValidator _confirmOrderValidator;


        public ShoppingCartService(IConfirmOrderValidator confirmOrderValidator, IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
            _confirmOrderValidator = confirmOrderValidator;
        }


        public async Task<bool> CheckoutAsync(ConfirmOrderAddViewModel confirmOrderAddViewModel)
        {
            bool isSuccess = false;
            Voucher vourcher = new Voucher();
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;


            // get status
            //string? orderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1);
            //string? paymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1);

            //if (orderStatus == null && paymentStatus == null)
            //{
            //    return false;
            //}

            // get 
            List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);
            decimal? totalPrice = 0;
            decimal? beforeVoucherPrice = 0;
            foreach (var x in shoppingCarts)
            {
                beforeVoucherPrice += (x.Product.PriceAfterDiscount * x.Count);
            }
            //
            if (confirmOrderAddViewModel.VourcherCode != null)
            {
                // get discount of voucher and update Total
                vourcher = await _unitOfWork.VoucherRepository.GetVoucherByCodeAsync(confirmOrderAddViewModel.VourcherCode);
                totalPrice = beforeVoucherPrice - beforeVoucherPrice * vourcher.DiscountPercent;
            }
            else
            {

                totalPrice = beforeVoucherPrice;
            }
            //
            //+update to orderdetail
            List<OrderDetail> orderDetail = new List<OrderDetail>();
            foreach (var x in shoppingCarts)
            {
                OrderDetail _orderDetail = new OrderDetail()
                {

                    ProductId = x.ProductId,
                    Quantity = x.Count,
                    Price = (x.Product.PriceAfterDiscount * x.Count)


                };
                orderDetail.Add(_orderDetail);
                //await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);

            }

            // order detail

            /// update quatity in stock of product 
            /// 

            /// update quatity in stock of product 

            Order order = new Order();
            if (confirmOrderAddViewModel.PaymentMethod.Equals(PaymentMethod.COD))
            {
                if (confirmOrderAddViewModel.VourcherCode != null)
                {
                    order = new Order
                    {
                        ApplicationUserId = currentUserId,
                        NameRecieved = confirmOrderAddViewModel.Name,
                        OrderDate = _timeService.GetCurrentTimeInVietnam(),
                        TotalPriceBeforeVoucher = beforeVoucherPrice,
                        TotalPrice = totalPrice,
                        OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending
                        PhoneNumber = confirmOrderAddViewModel.Phone,
                        StreetAddress = confirmOrderAddViewModel.StreetAddress,
                        City = confirmOrderAddViewModel.City,
                        PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending
                        //VoucherId = vourcher.Id,
                        VoucherCode = confirmOrderAddViewModel.VourcherCode,
                        Details = orderDetail
                    };
                }
                else
                {

                    order = new Order
                    {
                        ApplicationUserId = currentUserId,
                        NameRecieved = confirmOrderAddViewModel.Name,
                        OrderDate = _timeService.GetCurrentTimeInVietnam(),
                        TotalPrice = totalPrice,
                        OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(1),  // pending
                        PhoneNumber = confirmOrderAddViewModel.Phone,
                        StreetAddress = confirmOrderAddViewModel.StreetAddress,
                        City = confirmOrderAddViewModel.City,
                        PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(6),   // cod
                        Details = orderDetail


                    };
                }
            }
            else if (confirmOrderAddViewModel.PaymentMethod.Equals(PaymentMethod.PAYONLINE))
            {
                if (confirmOrderAddViewModel.VourcherCode != null)
                {
                    order = new Order
                    {
                        ApplicationUserId = currentUserId,
                        NameRecieved = confirmOrderAddViewModel.Name,
                        OrderDate = _timeService.GetCurrentTimeInVietnam(),
                        TotalPrice = totalPrice,
                        OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(2),  // approved,
                        PhoneNumber = confirmOrderAddViewModel.Phone,
                        StreetAddress = confirmOrderAddViewModel.StreetAddress,
                        City = confirmOrderAddViewModel.City,
                        PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,
                        //VoucherId = vourcher.Id,
                        Details = orderDetail
                    };
                }
                else
                {

                    order = new Order
                    {
                        ApplicationUserId = currentUserId,
                        NameRecieved = confirmOrderAddViewModel.Name,
                        OrderDate = _timeService.GetCurrentTimeInVietnam(),
                        TotalPrice = totalPrice,
                        OrderStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(2),  // approved,,
                        PhoneNumber = confirmOrderAddViewModel.Phone,
                        StreetAddress = confirmOrderAddViewModel.StreetAddress,
                        City = confirmOrderAddViewModel.City,
                        PaymentStatus = await _unitOfWork.StatusRepository.GetStatusStateByIdAsync(7),   // PAYONLINE,,
                        Details = orderDetail


                    };
                }


            }



            await _unitOfWork.OrderRepository.AddAsync(order);



            //+delete shopping cart
            await _unitOfWork.ShoppingCartRepository.DeleteShoppingCartsByUserIdAsync(currentUserId);

            return await _unitOfWork.SaveChangesAsync();
        }




        public async Task<bool> CreateOrUpdateAsync(int productId, int count)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            var cartItem = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, productId);
            if (cartItem == null)
            {
                cartItem = new ShoppingCart
                {
                    ProductId = productId,
                    Count = count,
                    ApplicationUserId = currentUserId,
                    CreatedAt = _timeService.GetCurrentTimeInVietnam()
                };
                _unitOfWork.ShoppingCartRepository.AddAsync(cartItem);
                return await _unitOfWork.SaveChangesAsync();


            }
            cartItem.Count += count;
            cartItem.ModifiedAt = _timeService.GetCurrentTimeInVietnam();
            _unitOfWork.ShoppingCartRepository.Update(cartItem);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductViewModel> ExistProductAsync(int productId)
        {
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            return _mapper.Map<ProductViewModel>(result);
        }

        public async Task<bool> ExistProductByIdAndUserIdAsync(int productId)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;

            //

            var cartItem = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, productId);
            if (cartItem is null) return false;
            return true;

        }



        //public async Task<ProductViewModel?> GetProductByIdAsync(int productId)
        //{
        //    var result = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
        //    return _mapper.Map<ProductViewModel>(result);
        //}

        public Task<ShoppingCartViewModel?> GetShoppingCartByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShoppingCartViewModel>> GetShoppingCartsAsync()
        {
            //

            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return new List<ShoppingCartViewModel>();


            var result = await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);

            return _mapper.Map<List<ShoppingCartViewModel>>(result);
            //return shoppingCartViewModels;
        }

        public async Task<bool> RemoveFromCartAsync(int productId)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;

            //

            var cartItem = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, productId);
            if (cartItem is null) return false;
            _unitOfWork.ShoppingCartRepository.Delete(cartItem);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ValidationResult> ValidateConfirmOrderAddAsync(ConfirmOrderAddViewModel vm)
        {
            return await _confirmOrderValidator.ConfirmOrderAddValidator.ValidateAsync(vm);
        }
    }
}
