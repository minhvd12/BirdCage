using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Validator
{
    public class UserValidator : IUserValidator
    {
        private readonly UserSignUpRule _userSignUpValidator;
        private readonly UserChangePasswordRule _userChangePassswordValidator;
        private readonly UpdateProfileRule _userUpdateProfileValidator;
        private readonly ResetPasswordRule _resetPasswordValidator;

        public UserValidator(ResetPasswordRule resetPasswordValidator, UserSignUpRule userSignUpValidator, UserChangePasswordRule userChangePassswordValidator, UpdateProfileRule userUpdateProfileValidator)
        {
            _userSignUpValidator = userSignUpValidator;
            _userChangePassswordValidator = userChangePassswordValidator;
            _userUpdateProfileValidator = userUpdateProfileValidator;   
            _resetPasswordValidator = resetPasswordValidator;
        }
        public IValidator<UserSignUpViewModel> UserSignUpValidator => _userSignUpValidator;

        public IValidator<UserChangePasswordViewModel> UserChangePasswordValidator => _userChangePassswordValidator;

        public IValidator<UpdateProfileViewModel> UserUpdateProfileValidator => _userUpdateProfileValidator;
        public IValidator<ResetPassword> ResetPasswordValidator => _resetPasswordValidator;
    }
}
