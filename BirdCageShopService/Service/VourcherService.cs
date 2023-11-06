using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.UtilMethod;
using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class VourcherService : BaseService, IVourcherService
    {

        private readonly IVoucherValidator _voucherValidator;

        public VourcherService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
        }

        //public VourcherService(ITimeService timeService,IVoucherValidator voucherValidator, IUnitOfWork unitOfWork, IMapper mapper, IUserValidator userValidator, IConfiguration configuration) : base(timeService,unitOfWork, mapper, configuration)
        //{
        //          _voucherValidator = voucherValidator;
        //      }

        public async Task<bool> CreateNewAsync(VourcherAddViewModel vm)
        {
            var voucher = _mapper.Map<Voucher>(vm);
            voucher.VoucherCode = VoucherCodeGenerator.GenerateUniqueVoucherCode();
            await _unitOfWork.VoucherRepository.AddAsync(voucher);
            return await _unitOfWork.SaveChangesAsync();
        }

        // get vourcher have expdate > datetime now
        public async Task<IEnumerable<VourcherViewModel>> GetVourcherAsync()
        {
            var vourchers = await _unitOfWork.VoucherRepository.GetAllAsync();
            return _mapper.Map<List<VourcherViewModel>>(vourchers);
        }

        public async Task<VourcherViewModel?> GetVourcherByCodeAsync(string code)
        {
            var vourchers = await _unitOfWork.VoucherRepository.GetVoucherByCodeAsync(code);
            return _mapper.Map<VourcherViewModel>(vourchers);
        }

        public Task<ValidationResult> ValidateVourcherAdddpAsync(VourcherAddViewModel vm)
        {
            return _voucherValidator.VourcherAddValidator.ValidateAsync(vm);
        }
    }
}
