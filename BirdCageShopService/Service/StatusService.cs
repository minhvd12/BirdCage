using AutoMapper;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Status;
using BirdCageShopViewModel.Voucher;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class StatusService : BaseService, IStatusService
    {
        public StatusService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
        }

        public async Task<IEnumerable<StatusViewModel>> GetStatusAsync()
        {
            var status = await _unitOfWork.StatusRepository.GetAllAsync();
            return _mapper.Map<List<StatusViewModel>>(status);
        }

        public async Task<string?> GetStatusByIdAsync(int id)
        {
            var status = await _unitOfWork.StatusRepository.GetByIdAsync(id);
            var rs = status.StatusState;
            return rs;
        }
    }
}
