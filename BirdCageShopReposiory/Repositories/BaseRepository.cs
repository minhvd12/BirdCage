using BirdCageShopDbContext;
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
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        protected readonly BirdCageShopContext _context;
        protected readonly DbSet<TModel> _entities;
        public BaseRepository(BirdCageShopContext context)
        {
            _context = context;
            _entities = context.Set<TModel>();
        }
        public TModel FirstOrDefault(Expression<Func<TModel, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate) ?? null!;
        }

        public async Task<TModel> FirstOrDefaultAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _entities.FirstOrDefaultAsync(predicate) ?? null!;
        }
        public async Task AddAsync(TModel entity)
        {
            await _context.Set<TModel>().AddAsync(entity);
        }

        public void Delete(TModel entity)
        {
            _context.Set<TModel>().Remove(entity);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await _context.Set<TModel>().ToListAsync();
        }



        public virtual async Task<Pagination<TModel>> GetAllByConditionAsync(Expression<Func<TModel, bool>> filters, int pageIndex, int pageSize)
        {


            var totalCount = await _context.Set<TModel>().CountAsync();
            var items = await _context.Set<TModel>()
                .AsNoTracking()
                .Where(filters)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<TModel>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }

        public virtual async Task<TModel?> GetByIdAsync(int id)
        {
            return await _context.Set<TModel>().FindAsync(id);
        }

        public virtual async Task<Pagination<TModel>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var totalCount = await _context.Set<TModel>().CountAsync();
            var items = await _context.Set<TModel>()
                .AsNoTracking()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<TModel>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }

        public void Update(TModel entity)
        {
            _context.Set<TModel>().Update(entity);
        }
    }
}
