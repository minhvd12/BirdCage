
using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetByStringIdAsync(string id);
        //Task<User?> GetUserByEmailAsync(string email);
        //Task<string?> GetRoleNameByUserIdAsync(int id);
        //Task<User?> AuthorizeAsync(string email, string password);
    }
}

