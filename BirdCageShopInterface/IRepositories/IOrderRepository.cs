using BirdCageShopDbContext.Models;
using BirdCageShopViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        //Task<Order> AddAsync(Order order);
        Task<Order?> GetByIdToUpdateStatusToApprovedAsync(int id);

        
        Task<Order?> GetByIdToUpdateStatusToShippedAsync(int id);


        Task<Order?> GetByIdToUpdateStatusPayToApprovedAsync(int id);
    }
}
