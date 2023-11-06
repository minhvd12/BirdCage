using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.User
{
    public class UserChangePasswordViewModel
    {
        public string OldPassword { get; set; }


        public string NewPassword { get; set; }


        public string ConfirmPassword { get; set; }
    }
    public class UserChangePasswordRule : AbstractValidator<UserChangePasswordViewModel>
    {
        public UserChangePasswordRule()
        {
            RuleFor(x => x.OldPassword)
             .NotEmpty()
             .WithMessage("Password must not be empty")
             .Length(6, 20)
             .WithMessage("Password must be between 6 and 20 characters")
             .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$")
             .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
             .WithMessage("Password must not be empty")
             .Length(6, 20)
             .WithMessage("Password must be between 6 and 20 characters")
             .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$")
             .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password must not be empty")
               .Length(6, 20)
                .WithMessage("New password must between 6..20 characters")
                .Equal(x => x.NewPassword)
                .WithMessage("Confirm password must match the new password")
                 .Length(6, 20)
                 .WithMessage("Password must be between 6 and 20 characters")
                 .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$")
                 .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.");

        }
    }
}
