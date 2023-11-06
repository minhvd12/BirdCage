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
    public class ProductFeatureRepository : BaseRepository<ProductFeature>, IProductFeatureRepository
    {
        public ProductFeatureRepository(BirdCageShopContext context) : base(context)
        {
        }
    }
}
