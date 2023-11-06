using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(BirdCageShopContext context) : base(context)
        {
        }

        public async Task<string?> GetStatusStateByIdAsync(int id)
        {
            var status = await _context.Set<Status>().FirstOrDefaultAsync(x => x.Id == id);
            var rs = status.StatusState;
            return rs;
        }
    }
}
