using BirdCageShopViewModel.Category;
using BirdCageShopViewModel.Role;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IValidator
{
    public interface ICategoryValidator
    {
        IValidator<CategoryCreateViewModel> CategoryAddValidator { get; }
    }
}
