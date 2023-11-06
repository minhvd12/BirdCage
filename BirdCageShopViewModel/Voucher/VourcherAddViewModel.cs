using BirdCageShopViewModel.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Voucher
{
    public class VourcherAddViewModel
    {
        public decimal? DiscountPercent { get; set; }

        public DateTime ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }
    }
    public class VourcherAddRule : AbstractValidator<VourcherAddViewModel>
    {
        public VourcherAddRule()
        {
            RuleFor(x => x.DiscountPercent)
    .NotEmpty()
    .WithMessage("Discount percent must not be empty")
    .InclusiveBetween(0, 1)
    .WithMessage("Discount percent must be between 0 and 1 (inclusive)");


            RuleFor(x => x.StartDate)
              .NotEmpty()
              .WithMessage("StartDate must not be empty");



        }
    }
}
