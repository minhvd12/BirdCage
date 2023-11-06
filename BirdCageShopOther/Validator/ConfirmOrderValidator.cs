using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Role;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Validator
{
    public class ConfirmOrderValidator : IConfirmOrderValidator
    {
        private readonly ConfirmOrderAddRule _confirmOrderAddValidator;

        public ConfirmOrderValidator(ConfirmOrderAddRule confirmOrderAddValidator)
        {
            _confirmOrderAddValidator = confirmOrderAddValidator;
        }
        public IValidator<ConfirmOrderAddViewModel> ConfirmOrderAddValidator => _confirmOrderAddValidator;


    }
}
