using birdcageshopinterface.IServices;
using BirdCageShopViewModel.Status;
using BirdCageShopViewModel.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IStatusService : IBaseService
    {
        Task<string?> GetStatusByIdAsync(int id);
        Task<IEnumerable<StatusViewModel>> GetStatusAsync();
    }
}
