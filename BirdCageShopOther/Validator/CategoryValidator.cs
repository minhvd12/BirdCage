using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Category;
using BirdCageShopViewModel.Role;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Validator
{
    public class CategoryValidator : ICategoryValidator
    {

        private readonly CategoryCreateRule _categoryAddValidator;

        public CategoryValidator(CategoryCreateRule categoryAddValidator)
        {
            _categoryAddValidator = categoryAddValidator;
        }



        public IValidator<CategoryCreateViewModel> CategoryAddValidator => _categoryAddValidator;
    }
}
