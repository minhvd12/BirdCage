using BirdCageShopDomain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Orders = new HashSet<Order>();
            ProductReviews = new HashSet<ProductReview>();
            Wishlists = new HashSet<Wishlist>();
        }

    
      
   
      
        public DateTime DoB { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Gender { get; set; }

        public bool IsDelete { get; set; }


        //public virtual Wishlist? Wishlist { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
		public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
		public IEnumerable<BankAccount> BankAccounts { get; set; }  
    }
}
