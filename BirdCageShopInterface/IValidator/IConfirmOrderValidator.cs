using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Role;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IValidator
{
    public interface IConfirmOrderValidator
    {
        IValidator<ConfirmOrderAddViewModel> ConfirmOrderAddValidator { get; }

    }
}
