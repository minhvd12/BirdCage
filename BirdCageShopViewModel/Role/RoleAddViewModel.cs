using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Role
{
    public class RoleAddViewModel
    {
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class RoleAddRule: AbstractValidator<RoleAddViewModel>
    {
        public RoleAddRule() {
        
            // role name
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role name must not be empty");

            //decription
            
        }    
    }
}
