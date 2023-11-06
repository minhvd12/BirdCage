using BirdCageShopViewModel.Voucher;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.User
{
    public class UpdateProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? DoB { get; set; }
    }

    public class UpdateProfileRule : AbstractValidator<UpdateProfileViewModel>
    {
        public UpdateProfileRule()
        {
            RuleFor(x => x.UserName)
        .NotEmpty().WithMessage("UserName must not be empty");



            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email must not be empty")
           .EmailAddress().WithMessage("Invalid email format");

       





        }
    }
}
