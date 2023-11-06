using BirdCageShopDomain.Models;
using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class Specification
    {
        public int Id { get; set; }
        public string SpecificationName { get; set; } = null!;
        public string SpecificationValue { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModiedAt { get; set; }
        public decimal? Price { get; set; }
        //public int? ProductId { get; set; }

        //public virtual Product? Product { get; set; }
        public bool IsDelete { get; set; }
        public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; }
    }
}
