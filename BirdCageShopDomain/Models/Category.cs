using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    
    public partial class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDelete { get; set; }



        public IEnumerable<Product> Products { get; set; }
    }
}
