using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<string?> GetStatusStateByIdAsync(int id);
    }
}
