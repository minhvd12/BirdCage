using BirdCageShopViewModel.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Order
{
    public class ConfirmOrderAddViewModel
    {
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string? VourcherCode { get; set; }
        public string PaymentMethod { get; set; }

        public string Name { get; set; }
    }

    public class ConfirmOrderAddRule : AbstractValidator<ConfirmOrderAddViewModel>
    {
        public ConfirmOrderAddRule()
        {
            RuleFor(c => c.Phone)
                .NotEmpty()
                .WithMessage("Phone can not be empty");
            RuleFor(c => c.Name)
              .NotEmpty()
              .WithMessage("Name can not be empty");
            RuleFor(c => c.StreetAddress)
              .NotEmpty()
              .WithMessage("StreetAddress can not be empty");
            RuleFor(c => c.City)
              .NotEmpty()
              .WithMessage("City can not be empty");
            RuleFor(c => c.PaymentMethod)
            .NotEmpty()
            .WithMessage("PaymentMethod can not be empty");

        }
    }
}
