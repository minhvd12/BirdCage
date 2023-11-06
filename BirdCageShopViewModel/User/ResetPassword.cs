using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.User
{
    public class ResetPassword
    {
        //[Required]
        public string Password { get; set; }

        //[Compare("Password", ErrorMessage = "The password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
    public class ResetPasswordRule : AbstractValidator<ResetPassword>
    {
        public ResetPasswordRule()
        {

            RuleFor(x => x.Password)
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
                .Equal(x => x.Password)
                .WithMessage("Confirm password must match the new password")
                 .Length(6, 20)
                 .WithMessage("Password must be between 6 and 20 characters")
                 .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$")
                 .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.");

        }
    }
}
