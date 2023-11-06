//using AutoMapper;
//using BirdCageShopDbContext.Models;
//using BirdCageShopInterface;
//using BirdCageShopInterface.IServices;
//using BirdCageShopInterface.IValidator;
//using BirdCageShopViewModel.Category;
//using BirdCageShopViewModel.Role;
//using FluentValidation.Results;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BirdCageShopService.Service
//{
//    public class CategoryService : BaseService, ICategoryService
//    {

//        private readonly ICategoryValidator _categoryValidator;

//		public CategoryService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
//		{
//		}

//		//public CategoryService(ICategoryValidator categoryValidator, IUnitOfWork unitOfWork, IMapper mapper, IUserValidator userValidator, IConfiguration configuration,ITimeService timeService) : base( timeService,unitOfWork, mapper, configuration)
//		//{
//		//          _categoryValidator = categoryValidator; 
//		//      }

//		public async Task<bool> CreateAsync(CategoryCreateViewModel vm)
//        {
//            var category = _mapper.Map<Category>(vm);
//            category.CreateAt = DateTime.Now;
//            await _unitOfWork.CategoryRepository.AddAsync(category);
//            return await _unitOfWork.SaveChangesAsync();
//        }

//        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
//        {
//            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
//            return _mapper.Map<List<CategoryViewModel>>(categories);
//        }

       

//        public async Task<bool> isExistNameCategory(string name)
//        {
//            var isExists = await _unitOfWork.CategoryRepository.GetByNameAsync(name);
//            if (isExists != null)
//            {
//                return true;
//            }
//            return false;
//        }

//        public Task<ValidationResult> ValidateCategoryCreateAsync(CategoryCreateViewModel vm)
//        {
//            return _categoryValidator.CategoryAddValidator.ValidateAsync(vm);
//        }
//    }
//}
