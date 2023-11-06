using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {

        public OrderRepository(BirdCageShopContext context) : base(context)
        {
        }


        public override async Task<Pagination<Order>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var source = _context.Set<Order>()
               .Include(o => o.Details).Include(o => o.ApplicationUser);
            //
            var totalCount = await source.CountAsync();
            var items = await source
                .AsNoTracking()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<Order>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }

        public virtual async Task<Pagination<Order>> GetAllByConditionAsync(Expression<Func<Order, bool>> filters, int pageIndex, int pageSize)
        {

            var totalCount = await _context.Set<Order>().CountAsync();
            var items = await _context.Set<Order>()
                .AsNoTracking()
                .Where(filters)
                   .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                     .ThenInclude(p => p.ProductImages)
                   .Include(o => o.ApplicationUser)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<Order>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }
        public override async Task<Order?> GetByIdAsync(int id)
        {

         

            return await _context.Set<Order>()
    .AsNoTracking()
     .AsNoTrackingWithIdentityResolution()
    .Where(x => x.Id == id)
    .Include(od => od.Details)
    .ThenInclude(d => d.Product)
    .Include(u => u.ApplicationUser)
    .FirstOrDefaultAsync();
       
        }

        public async Task<Order?> GetByIdToUpdateStatusToApprovedAsync(int id)
        {

            return await _context.Set<Order>()
               .AsNoTracking()
               .AsNoTrackingWithIdentityResolution()
               .Include(x => x.Details)
               .ThenInclude(d => d.Product)
               .Include(x => x.ApplicationUser)
              .FirstOrDefaultAsync(x => x.Id == id && x.OrderStatus == "Pending" && (x.PaymentStatus == "COD" || x.PaymentStatus == "Payonline-approved"));   // will fix not hard code later on
        }

        public async Task<Order?> GetByIdToUpdateStatusToShippedAsync(int id)
        {
            return await _context.Set<Order>()
                .AsNoTracking()
                .AsNoTrackingWithIdentityResolution()
                .Include(x => x.Details)
                .ThenInclude(d => d.Product)
                .Include(x => x.ApplicationUser)
               .FirstOrDefaultAsync(x => x.Id == id && x.OrderStatus == "Approved" && (x.PaymentStatus == "COD" || x.PaymentStatus == "Payonline-approved"));   // will fix not hard code later on
        }

        public async Task<Order?> GetByIdToUpdateStatusPayToApprovedAsync(int id)
        {
            return await _context.Set<Order>()
             .AsNoTracking()
             .AsNoTrackingWithIdentityResolution()
             .Include(x => x.Details)
             .ThenInclude(d => d.Product)
             .Include(x => x.ApplicationUser)
            .FirstOrDefaultAsync(x => x.Id == id && x.OrderStatus == "Shipped" && x.PaymentStatus == "COD");   // will fix not hard code later on
        }

        //public async Task<Order> AddAsync(Order order)
        //{
        //    await _context.Orders.AddAsync(order);
        //    return order;
        // }



    }
}
