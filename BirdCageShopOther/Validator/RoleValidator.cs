using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Role;
using FluentValidation;

namespace BirdCageShopOther.Validator
{
    public class RoleValidator : IRoleValidator
    {
        private readonly RoleAddRule _roleAddValidator;

        public RoleValidator(RoleAddRule roleAddValidator)
        {
            _roleAddValidator = roleAddValidator;
        }



        public IValidator<RoleAddViewModel> RoleAddValidator => _roleAddValidator;
    }
}
