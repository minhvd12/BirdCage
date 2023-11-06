using BirdCageShopDbContext;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BirdCageShopContext _context;

        private IProductFeatureRepository _productFeatureRepository;
        //private IRoleRepository _roleRepository;
        private ICategoryRepository _categoryRepository;
        private IReviewRepository _reviewRepository;
        private IUserRepository _userRepository;
        private IVoucherRepository _voucherRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        private IWishlistRepository _wishlistRepository;
        private IStatusRepository _statusRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IBirdCageTypeRepository _birdCageTypeRepository;
        private IProductSpecificationsRepository _productSpecificationsRepository;
        private ISpecificationRepository _specificationRepository;
        private IFeatureRepository _featureRepository;
        private IProductImageRepository _productImageRepository;
        //public UnitOfWork(BirdCageShopContext context, IVoucherRepository voucherRepository,
        //    IWishlistRepository wishlistRepository, IOrderRepository orderRepository,
        //    ICategoryRepository categoryRepository, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository, IStatusRepository statusRepository
        //    , IOrderDetailRepository orderDetailRepository)
        //{
        //    _context = context;
        //    //_roleRepository = roleRepository;
        //    //_userRepository = userRepository;
        //    _voucherRepository = voucherRepository;
        //    _categoryRepository = categoryRepository;
        //    _orderRepository = orderRepository;
        //    _productRepository = productRepository;
        //    _shoppingCartRepository = shoppingCartRepository;
        //    _wishlistRepository = wishlistRepository;
        //    _statusRepository = statusRepository;
        //    _orderDetailRepository = orderDetailRepository;


        //}


        public UnitOfWork(BirdCageShopContext context, IReviewRepository reviewRepository, IVoucherRepository voucherRepository,
            IWishlistRepository wishlistRepository, IOrderRepository orderRepository,
            ICategoryRepository categoryRepository, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository, IStatusRepository statusRepository
            , IOrderDetailRepository orderDetailRepository, IUserRepository userRepository, IBirdCageTypeRepository birdCageTypeRepository,
            IProductSpecificationsRepository productSpecificationsRepository,
            IProductFeatureRepository productFeatureRepository, ISpecificationRepository specificationRepository, IFeatureRepository featureRepository, IProductImageRepository productImageRepository)
        {
            _context = context;
            //_roleRepository = roleRepository;
            _userRepository = userRepository;
            _voucherRepository = voucherRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _wishlistRepository = wishlistRepository;
            _statusRepository = statusRepository;
            _orderDetailRepository = orderDetailRepository;
            _reviewRepository = reviewRepository;
            _birdCageTypeRepository = birdCageTypeRepository;
            _productSpecificationsRepository = productSpecificationsRepository;
            _productFeatureRepository = productFeatureRepository;
            _specificationRepository = specificationRepository;
            _featureRepository = featureRepository;
            _productImageRepository = productImageRepository;
        }

        //public IRoleRepository RoleRepository => _roleRepository;
        public IProductImageRepository ProductImageRepository => _productImageRepository;
        public ISpecificationRepository SpecificationRepository => _specificationRepository;
        public IFeatureRepository FeatureRepository => _featureRepository;
        public IProductFeatureRepository ProductFeatureRepository => _productFeatureRepository;
        public IStatusRepository StatusRepository => _statusRepository;
        public IReviewRepository ReviewRepository => _reviewRepository;

        public IUserRepository UserRepository => _userRepository;
        public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductRepository ProductRepository => _productRepository;

        public IVoucherRepository VoucherRepository => _voucherRepository;

        public IOrderRepository OrderRepository => _orderRepository;
        public IShoppingCartRepository ShoppingCartRepository => _shoppingCartRepository;
        public IWishlistRepository WishlistRepository => _wishlistRepository;
        public IBirdCageTypeRepository BirdCageTypeRepository => _birdCageTypeRepository;
        public IProductSpecificationsRepository ProductSpecificationsRepository => _productSpecificationsRepository;    

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
        private IDbContextTransaction _transaction1;
        public IDbContextTransaction Transaction()
        {
            _transaction1 = _context.Database.BeginTransaction();
            return _transaction1;
        }
    }
}
