using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class OrderService : BaseService, IOrderService

    {
        public OrderService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
        }

        public async Task<OrderWithDetailViewModel?> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderWithDetailViewModel>(result);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var result = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            return result;
        }



        //public async Task<Pagination<OrderWithDetailViewModel>> GetByOrderStatusPageAsync(int statusId, int pageIndex, int pageSize)
        //{
        //    var result = await _unitOfWork.OrderRepository.GetAllByConditionAsync(c => c.OrderStatus == categoryId, pageIndex, pageSize);
        //    return _mapper.Map<Pagination<ProductViewModel>>(result);
        //}

        //public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IUserValidator userValidator, IConfiguration configuration, ITimeService timeService) : base(timeService,unitOfWork, mapper, configuration)
        //{

        //      }

        public async Task<Pagination<OrderWithDetailViewModel>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.OrderRepository.GetPaginationAsync(pageIndex, pageSize);
            return _mapper.Map<Pagination<OrderWithDetailViewModel>>(result);
        }

        public async Task<Pagination<OrderWithDetailViewModel>> GetOrderByOderStatusPageAsync(int orderStatusId, int pageIndex, int pageSize)
        {
            var status = await _unitOfWork.StatusRepository.GetByIdAsync(orderStatusId);

            var result = await _unitOfWork.OrderRepository.GetAllByConditionAsync(c => c.OrderStatus == status.StatusState, pageIndex, pageSize);
            return _mapper.Map<Pagination<OrderWithDetailViewModel>>(result);
        }

        public async Task<bool> GetByIdToUpdateStatusToApprovedAsync(int id)
        {
            var result = await _unitOfWork.OrderRepository.GetByIdToUpdateStatusToApprovedAsync(id);
            if (result == null) return false;
            result.OrderStatus = "Approved";
            _unitOfWork.OrderRepository.Update(result);
            return await _unitOfWork.SaveChangesAsync();


        }

        public async Task<bool> GetByIdToUpdateStatusToShippeddAsync(int id)
        {
            var result = await _unitOfWork.OrderRepository.GetByIdToUpdateStatusToShippedAsync(id);
            if (result == null) return false;
            result.OrderStatus = "Shipped";
            _unitOfWork.OrderRepository.Update(result);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> GetByIdToUpdateStatusPayToApprovedAsync(int id)
        {
            var result = await _unitOfWork.OrderRepository.GetByIdToUpdateStatusPayToApprovedAsync(id);
            if (result == null) return false;
            result.PaymentStatus = "Approved";
            _unitOfWork.OrderRepository.Update(result);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _unitOfWork.OrderRepository.Update(order);
        }
    }
}
