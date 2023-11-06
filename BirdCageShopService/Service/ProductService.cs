using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Category;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ProductReviews;
using BirdCageShopViewModel.Role;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BirdCageShopService.Service
{
    public class ProductService : BaseService, IProductService

    {

        public ProductService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {

        }

        public async Task<ProductWithReviewViewModel?> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            return _mapper.Map<ProductWithReviewViewModel>(result);
        }

        public async Task<ProductWithReviewViewModel?> GetFeedBackByProductId(int productId)
        {

            var result = await _unitOfWork.ProductRepository.GetProductWithReviewByProIdAsync(productId);
            return _mapper.Map<ProductWithReviewViewModel>(result);
        }

        public async Task<Pagination<ProductViewModel>> GetPageAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.ProductRepository.GetPaginationAsync(pageIndex, pageSize);
            return _mapper.Map<Pagination<ProductViewModel>>(result);
        }

        public async Task<Pagination<ProductViewModel>> GetByBirdCageTypePageAsync(int birdCageTypeId, int pageIndex, int pageSize)
        {

            var result = await _unitOfWork.ProductRepository.GetAllByConditionAsync(c => c.BirdCageTypeId == birdCageTypeId, pageIndex, pageSize);
            return _mapper.Map<Pagination<ProductViewModel>>(result);

            //var result = await _unitOfWork.ProductRepository.GetAllByConditionAsync(pageIndex, pageSize);
            //return _mapper.Map<Pagination<ProductViewModel>>(result);
        }
        public async Task<Pagination<ProductViewModel>> GetByCagegoryTypePageAsync(int categoryId, int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.ProductRepository.GetAllByConditionAsync(c => c.CategoryId == categoryId, pageIndex, pageSize);
            return _mapper.Map<Pagination<ProductViewModel>>(result);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByCategoryAsync(int categoryId)
        {
            var result = await _unitOfWork.ProductRepository.GetProductsByCategoryAsync(categoryId);
            return _mapper.Map<List<ProductViewModel>>(result);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return _mapper.Map<List<ProductViewModel>>(products);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsFromWishlistAsync()
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return new List<ProductViewModel>();
            var result = await _unitOfWork.ProductRepository.GetProductsFromWishlistAsync(currentUserId);
            //var productFromWishlistList = new List<ProductFromWishlist>();
            //foreach (var product in result)
            //{
            //	var mainImageUrl = GetMainImageUrl(product);
            //	var productFromWishlist = new ProductFromWishlist
            //	{
            //		Title = product.Title,
            //		Category = new CategoryViewModel
            //		{
            //			// Populate CategoryViewModel properties if needed
            //		},
            //		PriceAfterDiscount = product.PriceAfterDiscount,
            //		ImageUrl = mainImageUrl // Set the main image URL
            //	};
            //	productFromWishlistList.Add(productFromWishlist);
            //}
            var productFromWishlistList = new List<ProductViewModel>();
            return _mapper.Map<List<ProductViewModel>>(result);



        }

        private string? GetMainImageUrl(Product product)
        {
            // Find the main product image
            var mainImage = product.ProductImages.FirstOrDefault(pi => pi.IsMainImage && pi.ImageUrl != null);


            if (mainImage != null)
            {
                return mainImage.ImageUrl;
            }
            else
            {
                // Handle case where no main image is found
                return null; // Or return a default image URL
            }
        }

        public async Task<bool> RemoveAsync(Product product)
        {
            product.isDelete = true;
            _unitOfWork.ProductRepository.Update(product);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> AddReviewProduct(int productId, AddReviewProductViewModel addReviewProductViewModel)
        {
            //check userId bought that product - have order status aprroved and payment: approved or Payonline-approved

            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;

            var product = await _unitOfWork.ProductRepository.GetProductWithReviewByProIdAsync(productId);

            ProductReview productReview = new ProductReview();
            productReview.Rating = addReviewProductViewModel.Rating;
            productReview.ReviewText = addReviewProductViewModel.ReviewText;
            productReview.ReviewDate = _timeService.GetCurrentTimeInVietnam();
            productReview.ApplicationUserId = currentUserId;
            productReview.ProductId = productId;
            //if (product != null) {

            //	product.ProductReviews = productReview;
            //}

            //product.ProductReviews.ToList().Add(productReview);


            _unitOfWork.ReviewRepository.AddAsync(productReview);


            return await _unitOfWork.SaveChangesAsync();


        }

        public async Task<Pagination<ProductViewModel>> GetByTilePageAsync(string title, int pageIndex, int pageSize)
        {
            var productList = await _unitOfWork.ProductRepository.GetAllByConditionAsync(c => c.Title.Contains(title), pageIndex, pageSize);
            return _mapper.Map<Pagination<ProductViewModel>>(productList);
        }



        public async Task<bool> AddToWishlistAsync(int productId)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;


            // check exist wishlist
            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistByCustomerIdAsync(currentUserId);
            if (wishlist == null)
            {
                var newWishlist = new Wishlist()
                {

                    ApplicationUserId = currentUserId,
                    ProductId = productId
                };
                _unitOfWork.WishlistRepository.AddAsync(newWishlist);

            }
            else
            {
                // check if exist that pro in wishlist
                var product = await _unitOfWork.ProductRepository.GetProductByProductIdAndCustomerIdAsync(currentUserId, productId);
                if (product != null) return false;
                //wishlist.WishlistItems.Append(new WishlistItem()
                //{
                //	ProductId = productId
                //});

                Wishlist addNew = new Wishlist()
                {
                    ProductId = productId,
                    ApplicationUserId = currentUserId
                };
                _unitOfWork.WishlistRepository.AddAsync(addNew);



            }

            return await _unitOfWork.SaveChangesAsync();


        }

        public async Task<bool> RemoveProductFromWishlistAsync(int productId)
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;


            // check exist wishlist
            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistByCustomerIdAsync(currentUserId);
            if (wishlist == null)
            {
                return false;
                //var newWishlist = new Wishlist()
                //{

                //    ApplicationUserId = currentUserId,
                //    ProductId = productId
                //};
                //_unitOfWork.WishlistRepository.AddAsync(newWishlist);

            }
            else
            {

                var wishlistItem = await _unitOfWork.WishlistRepository.GetWishlistByCustomerIdAndProductIdAsync(currentUserId, productId);
                if (wishlistItem == null) return false;

                _unitOfWork.WishlistRepository.Delete(wishlistItem);



            }

            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> MoveProFromWishlistToShoppingCart()
        {
            var currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            var wishList = await _unitOfWork.WishlistRepository.GetWishlistByCustomerIdAsync(currentUserId);
            if (wishList == null) return false;
          
            foreach(var wish in wishList) {
                var existProIdInShoppingCart = await _unitOfWork.ShoppingCartRepository.GetCartItemByUserIdAndProDIdAsync(currentUserId, wish.ProductId);
                if(existProIdInShoppingCart == null)
                {
                    ShoppingCart s = new ShoppingCart()
                    {
                        Count = 1,
                        CreatedAt = _timeService.GetCurrentTimeInVietnam(),
                        ProductId = wish.ProductId,
                        ApplicationUserId = currentUserId,
                    };
                    _unitOfWork.ShoppingCartRepository.AddAsync(s);
                    _unitOfWork.WishlistRepository.Delete(wish);
                }
               
            }


            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IActionResult> CreateProductAsync(CreateProductViewModel requestBody)
        {
            var transaction = _unitOfWork.Transaction();
            var category = await _unitOfWork.CategoryRepository.FirstOrDefaultAsync(c => c.Id == requestBody.CategoryId);

            if (requestBody.files == null || requestBody.files.Count > 10 || requestBody.files.Count == 0)
            {
                return new StatusCodeResult(300);
            }

            if (category == null)
            {
                return new StatusCodeResult(404);
            }

            var birdType = await _unitOfWork.BirdCageTypeRepository.FirstOrDefaultAsync(b => b.Id == requestBody.BirdCageTypeId);

            if (birdType == null)
            {
                return new StatusCodeResult(404);
            }
            
            var product = new Product
            {
                Title = requestBody.Title,
                Description = requestBody.Description,
                CategoryId = requestBody.CategoryId,
                BirdCageTypeId = requestBody.BirdCageTypeId,
                CreatedAt = DateTime.Now,
                isDelete = false,
                Price = requestBody.Price,
                SKU = requestBody.SKU,
                QuantityInStock = requestBody.QuantityInStock
            };

            if (requestBody.PercentDiscount != null)
            {
                product.PercentDiscount = requestBody.PercentDiscount / 100;
                product.PriceAfterDiscount = product.Price - (product.Price * product.PercentDiscount);
            }

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            foreach(var item in requestBody.ProductSpecifications)
            {
                var productSpecifications = await _unitOfWork.SpecificationRepository.FirstOrDefaultAsync(p => p.Id == item);

                if (productSpecifications == null)
                {
                    return new StatusCodeResult(404);
                }
                ProductSpecification productSpecification = new ProductSpecification
                {
                    ProductId = product.Id,
                    SpecificationId = productSpecifications.Id
                };

                await _unitOfWork.ProductSpecificationsRepository.AddAsync(productSpecification);
                await _unitOfWork.SaveChangesAsync();
            }

            if (requestBody.ProductFeature != null)
            {
                foreach (var feature in requestBody.ProductFeature)
                {
                    var productfeature = _unitOfWork.FeatureRepository.FirstOrDefault(p => p.Id == feature);

                    if (productfeature == null)
                    {
                        return new StatusCodeResult(404);
                    }     
                    
                    ProductFeature productFeature = new ProductFeature
                    {
                            ProductId = product.Id,
                            FeatureId = productfeature.Id
                    };

                    await _unitOfWork.ProductFeatureRepository.AddAsync(productFeature);
                    await _unitOfWork.SaveChangesAsync();
                  
                }
            }

            var firstFile = requestBody.files[0];
            foreach (var file in requestBody.files)
            {
                var imageItem = new ProductImage
                {
                    ImageUrl = await UploadProductImageToFirebase(file),
                    CreatedAt = DateTime.Now,
                    ProductId = product.Id,
                    IsMainImage = (file == firstFile)
                };

                await _unitOfWork.ProductImageRepository.AddAsync(imageItem);
            }

            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();
            return new OkObjectResult(await GetByIdAsync(product.Id));
        }
        public async Task<IActionResult> UpdateProductAsync(int id, UpdateProductViewModel requestBody)
        {
            var transaction = _unitOfWork.Transaction();
            Product exitedProduct = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if(exitedProduct == null)
            {
                return new StatusCodeResult(404);
            }
            var category = await _unitOfWork.CategoryRepository.FirstOrDefaultAsync(c => c.Id == requestBody.CategoryId);

            if (requestBody.files == null || requestBody.files.Count > 10 || requestBody.files.Count == 0)
            {
                return new StatusCodeResult(300);
            }

            if (category == null)
            {
                return new StatusCodeResult(404);
            }

            var birdType = await _unitOfWork.BirdCageTypeRepository.FirstOrDefaultAsync(b => b.Id == requestBody.BirdCageTypeId);

            if (birdType == null)
            {
                return new StatusCodeResult(404);
            }
            exitedProduct.Title = requestBody.Title;
            exitedProduct.Description = requestBody.Description;
            exitedProduct.CategoryId = requestBody.CategoryId;
            exitedProduct.BirdCageTypeId = requestBody.BirdCageTypeId;
            exitedProduct.ModifieldAt = DateTime.Now;
            exitedProduct.isDelete = false;
            exitedProduct.Price = requestBody.Price;
            exitedProduct.EditedBy = requestBody.EditedBy;
            exitedProduct.SKU = requestBody.SKU;
            exitedProduct.QuantityInStock = requestBody.QuantityInStock;

            if (requestBody.PercentDiscount != null)
            {
                exitedProduct.PercentDiscount = requestBody.PercentDiscount / 100;
                exitedProduct.PriceAfterDiscount = exitedProduct.Price - (exitedProduct.Price * exitedProduct.PercentDiscount);
            }

            _unitOfWork.ProductRepository.Update(exitedProduct);
            await _unitOfWork.SaveChangesAsync();

            foreach (var item in requestBody.ProductSpecifications)
            {
                var productSpecifications = await _unitOfWork.SpecificationRepository.FirstOrDefaultAsync(p => p.Id == item);

                if (productSpecifications == null)
                {
                    return new StatusCodeResult(404);
                }
                ProductSpecification productSpecification = new ProductSpecification
                {
                    ProductId = exitedProduct.Id,
                    SpecificationId = productSpecifications.Id
                };

                await _unitOfWork.ProductSpecificationsRepository.AddAsync(productSpecification);
                await _unitOfWork.SaveChangesAsync();
            }

            if (requestBody.ProductFeature != null)
            {
                foreach (var feature in requestBody.ProductFeature)
                {
                    var productfeature = _unitOfWork.FeatureRepository.FirstOrDefault(p => p.Id == feature);

                    if (productfeature == null)
                    {
                        return new StatusCodeResult(404);
                    }

                    ProductFeature productFeature = new ProductFeature
                    {
                        ProductId = exitedProduct.Id,
                        FeatureId = productfeature.Id
                    };

                    _unitOfWork.ProductFeatureRepository.Update(productFeature);
                    await _unitOfWork.SaveChangesAsync();

                }
            }

            var firstFile = requestBody.files[0];
            foreach (var file in requestBody.files)
            {
                var imageItem = new ProductImage
                {
                    ImageUrl = await UploadProductImageToFirebase(file),
                    CreatedAt = DateTime.Now,
                    ProductId = exitedProduct.Id,
                    IsMainImage = (file == firstFile)
                };

                await _unitOfWork.ProductImageRepository.AddAsync(imageItem);
            }

            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();
            return new OkObjectResult(await GetByIdAsync(exitedProduct.Id));
        }

        private async Task<string?> UploadProductImageToFirebase(IFormFile file)
        {
            var storage = new FirebaseStorage("swp2023-fc0eb.appspot.com");
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var imageUrl = await storage.Child("images")
                                        .Child(imageName)
                                        .PutAsync(file.OpenReadStream());
            return imageUrl;
        }
    }
}
