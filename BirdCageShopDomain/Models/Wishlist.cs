using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class Wishlist
    {
        public Wishlist()
        {
            //WishlistItems = new HashSet<WishlistItem>();
        }

        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        //public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }
        //public DateTime? ModifiedAt { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        //public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
