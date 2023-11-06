using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopDomain.Models
{
    public class ProductSpecification
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public int SpecificationId { get; set; }

        public Product Product { get; set; }
        public Specification Specification { get; set; }
    }
}
