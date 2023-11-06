using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class SpecificationRepository : BaseRepository<Specification>, ISpecificationRepository
    {
        public SpecificationRepository(BirdCageShopContext context) : base(context)
        {
        }
    }
}
