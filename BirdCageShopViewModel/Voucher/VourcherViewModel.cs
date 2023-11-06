using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Voucher
{
    public class VourcherViewModel
    {
        public int Id { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string VoucherCode { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
