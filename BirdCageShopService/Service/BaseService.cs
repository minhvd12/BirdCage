using AutoMapper;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly IClaimService _claimService;
        protected readonly IConfiguration _configuration;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ITimeService _timeService;
        public BaseService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _timeService = timeService;
            _claimService = claimService;
        }
    }
}
