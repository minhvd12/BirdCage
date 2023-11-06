using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Validator
{
    public class VoucherValidator : IVoucherValidator
    {
        private readonly VourcherAddRule _voucherAddValidator;

        public VoucherValidator(VourcherAddRule voucherAddValidator)
        {
            _voucherAddValidator = voucherAddValidator;
        }
        public IValidator<VourcherAddViewModel> VourcherAddValidator => _voucherAddValidator;
    }
}
