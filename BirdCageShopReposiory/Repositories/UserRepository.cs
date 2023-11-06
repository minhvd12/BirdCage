using BirdCageShopDbContext;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using BirdCageShopUtils.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCageShopUtils.UtilMethod;
using Microsoft.AspNetCore.Identity;

namespace BirdCageShopReposiory.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(BirdCageShopContext context) : base(context)
        {
        }

        //public async Task<User?> AuthorizeAsync(string email, string password)
        //{
        //	var admin = await _context.Set<User>()
        //	  .AsNoTracking()
        //            .Include(u => u.Role)
        //	  .FirstOrDefaultAsync(x => x.Email == email && !x.IsDelete);
        //	if (admin == null) return null;
        //	if (password.IsCorrectHashSource(admin.PasswordHash)) return admin;
        //	return null;
        //}

        public override async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var x =  await _context.Set<ApplicationUser>()
                .AsNoTracking()
                //.Where(x => x.IsDelete == false)
                .ToListAsync();


            //var y = await _context.Set<IdentityUser>()
            //    .AsNoTracking()
            //    .Where(x => x.User)
            //    //.Where(x => x.IsDelete == false)
            //    .ToListAsync();



            return x.ToList();  
            //     return await _context.Set<ApplicationUser>()
            //.AsNoTracking()
            //.Select(user => user.Gender)
            //.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser?> GetByStringIdAsync(string id)
        {
            var user = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsDelete == false);

            return user;

            

        }


        //public override async Task<Pagination<User>> GetPaginationAsync(int pageIndex, int pageSize)
        //{
        //    var source = _context.Set<User>()
        //       .Where(x => !x.IsDelete);
        //    //
        //    var totalCount = await source.CountAsync();
        //    var items = await source
        //        .AsNoTracking()
        //        .Skip(pageSize * pageIndex)
        //        .Take(pageSize)
        //        .ToListAsync();
        //    var result = new Pagination<User>()
        //    {
        //        Items = items,
        //        PageIndex = pageIndex,
        //        PageSize = pageSize,
        //        TotalItemsCount = totalCount
        //    };

        //    return result;
        //}

        //public async Task<string?> GetRoleNameByUserIdAsync(int id)
        //{
        //	var user = await _context.Set<User>().Include(u => u.Role).FirstOrDefaultAsync(x => x.Id == id && x.IsDelete == false);
        //          var rs = user.Role.RoleName;
        //	return rs;
        //}

        //public async Task<User?> GetUserByEmailAsync(string email)
        //{
        //    var user = await _context.Set<User>().FirstOrDefaultAsync(x => x.Email.Equals(email) && x.IsDelete == false);

        //    return user;
        //}
    }
}
