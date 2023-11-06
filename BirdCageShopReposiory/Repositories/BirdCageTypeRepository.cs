using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class BirdCageTypeRepository : BaseRepository<BirdCageType>, IBirdCageTypeRepository
    {
        public BirdCageTypeRepository(BirdCageShopContext context) : base(context)
        {
        }
    }
}
