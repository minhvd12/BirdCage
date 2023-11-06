using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IRepositories;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopReposiory.Repositories;
using BirdCageShopUtils.Pagination;
using BirdCageShopUtils.UtilMethod;
using BirdCageShopViewModel.Auth;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserValidator _userValidator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, UserManager<IdentityUser> userManager, IUserValidator userValidator, IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
            _userValidator = userValidator;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<Pagination<OrderWithDetailViewModel>> GetOrderHistoryAsync(int pageIndex, int pageSize)
        {

            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null)
            {

                return new Pagination<OrderWithDetailViewModel>
                {
                    Items = new List<OrderWithDetailViewModel>(),
                    TotalItemsCount = 0,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            var result = await _unitOfWork.OrderRepository.GetAllByConditionAsync(o => o.ApplicationUserId == currentUserId, pageIndex, pageSize);
            return _mapper.Map<Pagination<OrderWithDetailViewModel>>(result);
        }

        //public UserService(ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IUserValidator userValidator, IConfiguration configuration) : base(timeService,unitOfWork, mapper,configuration)
        //{
        //    //_roleValidator = roleValidator;
        //    _userValidator = userValidator;
        //}

        //public async Task<string?> AuthorizeAsync(SignInViewModel vm)
        //{
        //    var user = await _unitOfWork.UserRepository.AuthorizeAsync(vm.Email, vm.Password);
        //    if (user is null) return null;
        //    //if (_configuration == null) return null;
        //    //if (_timeService == null) return null;
        //    //if (user.Role.RoleName == null) return null;
        //    var roleName = await _unitOfWork.UserRepository.GetRoleNameByUserIdAsync(user.Id);
        //    var accessToken = user.GenerateToken(user.Id, _configuration, _timeService.GetCurrentTimeInVietnam(), 60 * 24 * 30, roleName);


        //    return accessToken;
        //}

        public async Task<bool> ChangePasswordAsync(UserChangePasswordViewModel vm)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            var user = await _userManager.FindByIdAsync(currentUserId);

            var result = await _userManager.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);


            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(ApplicationUser user)
        {
            user.IsDelete = true;
            _unitOfWork.UserRepository.Update(user);
            return await _unitOfWork.SaveChangesAsync();
        }




        public async Task<Pagination<UserViewModel>> GetPageAsync(int pageIndex, int pageSizes)
        {
            var result = await _unitOfWork.UserRepository.GetPaginationAsync(pageIndex, pageSizes);
            return _mapper.Map<Pagination<UserViewModel>>(result);
        }

        public async Task<IEnumerable<UserViewModel>> GetUserAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            var user = await _unitOfWork.UserRepository.GetByStringIdAsync(id);
            return user;
        }

        //public async Task<bool> IsExistsEmailAsync(string email)
        //{
        //    var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);

        //    return user != null;

        //}

        //public async Task<bool> RegisterAsync(UserSignUpViewModel vm)
        //{
        //    var user = _mapper.Map<User>(vm);
        //    user.PasswordHash = vm.Password.BCryptSaltAndHash();
        //    var role = await _unitOfWork.RoleRepository.GetByNameAsync("Customer");
        //    if(role != null) { 
        //        user.RoleId  = role.Id;
        //    }

        //    await _unitOfWork.UserRepository.AddAsync(user);
        //    return await _unitOfWork.SaveChangesAsync();


        //}

        public Task<ValidationResult> ValidateChangePasswordAsync(UserChangePasswordViewModel vm)
        {
            return _userValidator.UserChangePasswordValidator.ValidateAsync(vm);
        }

        public Task<ValidationResult> ValidateUserSignUpAsync(UserSignUpViewModel vm)
        {
            return _userValidator.UserSignUpValidator.ValidateAsync(vm);
        }

        public async Task<bool> UpdateProfileAsync(UpdateProfileViewModel vm)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            //var user = await _userManager.FindByIdAsync(currentUserId);
            var user = await _userRepository.GetByStringIdAsync(currentUserId);
            if (user == null) return false;


            user.UserName = vm.UserName;
            user.NormalizedUserName = vm.UserName.ToUpper();
            user.Email = vm.Email;
            user.NormalizedEmail = vm.Email.ToUpper();
            user.Gender = vm.Gender;
            user.PhoneNumber = vm.PhoneNumber;
            user.DoB = (DateTime)vm.DoB;
            

            //var userUpdate =  await _userManager.UpdateAsync(user);
            //if (userUpdate.Succeeded)
            //{
            //    return true;
            //}
            //return false;
            _unitOfWork.UserRepository.Update(user);

            return await _unitOfWork.SaveChangesAsync();


        }

        public async Task<bool> isExistUsername(string username)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user.UserName != username)
            {
                var check = await _userManager.FindByNameAsync(username);
                if (check != null) { return true; }
            }
            return false;

        }

        public async Task<bool> isExistEmail(string email)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user.Email != email)
            {
                var check = await _userManager.FindByEmailAsync(email);
                if (check != null) { return true; }
            }
            return false;
        }

        public Task<ValidationResult> ValidateUpdateProfileAsync(UpdateProfileViewModel vm)
        {
            return _userValidator.UserUpdateProfileValidator.ValidateAsync(vm);
        }

        public Task<ValidationResult> ValidateResetPasswordAsync(ResetPassword vm)
        {
            return _userValidator.ResetPasswordValidator.ValidateAsync(vm);
        }
    }
}
