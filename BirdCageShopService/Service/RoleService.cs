////using AutoMapper;
////using BirdCageShopDbContext.Models;
////using BirdCageShopInterface;
////using BirdCageShopInterface.IServices;
////using BirdCageShopInterface.IValidator;
////using BirdCageShopViewModel.Role;
////using FluentValidation.Results;
////using Microsoft.Extensions.Configuration;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;

////namespace BirdCageShopService.Service
////{
////    public class RoleService : BaseService, IRoleService
////    {
////        private readonly IRoleValidator _roleValidator;

////		public RoleService(IRoleValidator roleValidator,IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
////		{
////            _roleValidator = roleValidator;
////		}

////		//public RoleService(ITimeService timeService,IRoleValidator roleValidator, ICategoryValidator categoryValidator, IUnitOfWork unitOfWork, IMapper mapper, IUserValidator userValidator, IConfiguration configuration) : base(timeService,unitOfWork, mapper, configuration)
////		//{
////		//          _roleValidator = roleValidator;
////		//      }

////		//public async Task<bool> CreateAsync(RoleAddViewModel vm)
////  //      {
////  //          var role = _mapper.Map<Role>(vm);
////  //          role.CreateAt = DateTime.Now;
////  //          await _unitOfWork.RoleRepository.AddAsync(role);
////  //          return await _unitOfWork.SaveChangesAsync();



////  //      }

////        //public async Task<IEnumerable<RoleViewModel>> GetRolesAsync()
////        //{
////        //    var roles = await _unitOfWork.RoleRepository.GetAllAsync();
////        //    return _mapper.Map<List<RoleViewModel>>(roles);
////        //}

////        //public async Task<bool> isExistNameRole(string name)
////        //{
////        //    var isExists = await _unitOfWork.RoleRepository.GetByNameAsync(name);
////        //    if (isExists != null)
////        //    {
////        //        return true;
////        //    }
////        //    return false;
////        //}

////        public Task<ValidationResult> ValidateRoleAddAsync(RoleAddViewModel vm)
////        {
////            return _roleValidator.RoleAddValidator.ValidateAsync(vm);
////        }
////    }
////}
