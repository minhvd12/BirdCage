using BirdCageShopDomain.Models;
using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalPriceBeforeVoucher { get; set; }
        
        public DateTime? ShippingDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? PhoneNumber { get; set; }
        public string? NameRecieved { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? PaymentStatus { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ModifiedAt { get; set; }   
        public string? VoucherCode { get; set; }
              
        //public int? VoucherId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; } 
        //public virtual Voucher? Voucher { get; set; }
        //public virtual Status? Status { get; set; } = null!;
        public IEnumerable<OrderDetail> Details { get; set; }




    }
}
