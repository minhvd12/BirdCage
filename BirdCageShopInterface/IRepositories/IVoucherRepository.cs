using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        Task<Voucher?> GetVoucherByCodeAsync(string voucherCode);
    }
}
