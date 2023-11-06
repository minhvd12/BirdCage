
using birdcageshopinterface.IServices;
using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IVourcherService : IBaseService
    {
        Task<IEnumerable<VourcherViewModel>> GetVourcherAsync();
        Task<VourcherViewModel?> GetVourcherByCodeAsync(string code);
        Task<ValidationResult> ValidateVourcherAdddpAsync(VourcherAddViewModel vm);
        Task<bool> CreateNewAsync(VourcherAddViewModel vm);
    }
}
