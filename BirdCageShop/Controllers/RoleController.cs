//using BirdCageShopInterface.IServices;
//using BirdCageShopViewModel.Role;
//using FluentValidation.Results;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BirdCageShop.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
    
//    public class RoleController : ControllerBase
//    {
//        private readonly IRoleService _roleService;
//        public RoleController(IRoleService roleService) {
//            _roleService = roleService; 
//        }

//        [HttpGet]
//        //[Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Get()
//        {
//            var rs = await _roleService.GetRolesAsync();
//            return Ok(rs);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateAsync([FromBody] RoleAddViewModel vm)
//        {
//            var validateResult = await _roleService.ValidateRoleAddAsync(vm);
//            //
//            if (!validateResult.IsValid)
//            {
//                var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
//                return BadRequest(errors);
//            }
//            //
//            var isExistNameRole = await _roleService.isExistNameRole(vm.RoleName);
//            if (isExistNameRole) return StatusCode(StatusCodes.Status409Conflict, new { message = "That role name already exists" });

//            var isCreated = await _roleService.CreateAsync(vm);
//            if (isCreated == true) return Ok(vm);
//            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Create failed. Server Error" });

            
//        }
//        //[HttpPut]
//        //public async Task<IActionResult> CreateAsync([FromBody] RoleAddViewModel vm)
//        //{

//        //}

//    }
//}
