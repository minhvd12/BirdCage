using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IValidator
{
    public interface IVoucherValidator
    {
        IValidator<VourcherAddViewModel> VourcherAddValidator { get; }
    }
}
