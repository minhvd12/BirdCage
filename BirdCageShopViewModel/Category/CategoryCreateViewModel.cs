using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Category
{
    public class CategoryCreateViewModel
    {
        public string CategoryName { get; set; }
    }

    public class CategoryCreateRule : AbstractValidator<CategoryCreateViewModel>
    {
        public CategoryCreateRule()
        {
            RuleFor(c => c.CategoryName)
                .NotEmpty()
                .Length(3, 15)
                .WithMessage("Category's name only valid in range 3..15");
        }
    }
}
