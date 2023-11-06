using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopViewModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductReviewService _productReviewService;

        public ProductController(IProductService productService, IProductReviewService productReviewService)
        {
            _productService = productService;
            _productReviewService = productReviewService;       
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductViewModel createProductViewModel)
        {
            try
            {
                var createproduct = await _productService.CreateProductAsync(createProductViewModel);
                if (createproduct is StatusCodeResult statusCodeResult)
                {
                    if (statusCodeResult.StatusCode == 404) { return StatusCode(StatusCodes.Status404NotFound, "Thiếu thông tin"); }
                    else if (statusCodeResult.StatusCode == 300) { return StatusCode(StatusCodes.Status300MultipleChoices, "Số ảnh không được vượt quá 10 ảnh"); }
                    else { return StatusCode(StatusCodes.Status500InternalServerError, "có gì đó bị lỗi"); };
                }
                return createproduct;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi hệ thống");
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductViewModel updateProductViewModel)
        {
            try
            {
                var updateProduct = await _productService.UpdateProductAsync(id, updateProductViewModel);
                if (updateProduct is StatusCodeResult statusCodeResult)
                {
                    if (statusCodeResult.StatusCode == 404) { return StatusCode(StatusCodes.Status404NotFound, "Thiếu thông tin"); }
                    else if (statusCodeResult.StatusCode == 300) { return StatusCode(StatusCodes.Status300MultipleChoices, "Số ảnh không được vượt quá 10 ảnh"); }
                    else { return StatusCode(StatusCodes.Status500InternalServerError, "có gì đó bị lỗi"); };
                }
                return updateProduct;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi hệ thống");
            }
        }

        //get product by bird type
        [HttpGet("by-birdCage-type/{birdCageTypeId}")]
        public async Task<IActionResult> GetByBirdCageType([FromRoute] int birdCageTypeId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var pr = await _productService.GetByBirdCageTypePageAsync(birdCageTypeId, pageIndex, pageSize);
            return Ok(pr);
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryAsync([FromRoute] int categoryId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _productService.GetByCagegoryTypePageAsync(categoryId, pageIndex, pageSize);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _productService.GetPageAsync(pageIndex, pageSize);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product is null) return NotFound();
            var result = await _productService.RemoveAsync(product);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Delete product failed. Server Error." });
        }

     

        [HttpGet("search-by-title")]
        public async Task<IActionResult> GetProductByTitle(string title, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
          
            var result = await _productService.GetByTilePageAsync(title, pageIndex, pageSize);
            return Ok(result);
        }


        //[HttpGet("view-feedback/{productId}")]
        //public async Task<IActionResult> GetFeedBackByIdAsync([FromRoute] int productId)
        //{
        //    var product = await _productService.GetProductByIdAsync(productId);
        //    if (product is null) return NotFound("Not found this product");
        //    var result = await _productService.GetFeedBackByProductId(productId);
        //    return Ok(result);
        //}


        //delete feedback of product
        [HttpDelete("{productId}/reviews/{reviewId}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteReviewProductByProIdAndReviewIdAsync([FromRoute] int productId, [FromRoute] int reviewId)
        {
            //var product = await _productService.GetProductByIdAsync(productId);
            //if (product is null) return NotFound("Not found this product");
            var reviewOfProduct = await _productReviewService.GetProductReviewByProductIDAndReviewIdByIdAsync(productId, reviewId);
            if (reviewOfProduct is null) return NotFound("Not found this review");
            //
            //delete review
            bool isSuccess = await _productReviewService.DeleteReviewProduct(reviewOfProduct);

            if (isSuccess) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Delete review product failed. Server Error." });
        }



        [HttpPost("review-product/{productId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ReviewProductByIdAsync([FromRoute] int productId, [FromBody] AddReviewProductViewModel addReviewProductViewModel)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product is null) return NotFound("Not found this product");

            // create
            bool isSuccess = await _productService.AddReviewProduct(productId, addReviewProductViewModel);

            if (isSuccess) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Review product failed. Server Error." });
        }


        



        //
        [HttpGet("from-wishlist")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetFromWishlistAsync()
        {
            var result = await _productService.GetProductsFromWishlistAsync();
            return Ok(result);
        }



        [HttpPost("add-to-wishlist/{productId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToWishlistAsync([FromRoute] int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product is null) return BadRequest(new { property = "Product ID", message = "Product doesn't exist." });
            var result = await _productService.AddToWishlistAsync(productId);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Add to wishlist failed. Server Error." });
        }


        [HttpDelete("remove-product-from-wishlist/{productId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RemoveProductFromWishlistAsync([FromRoute] int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product is null) return BadRequest(new { property = "Product ID", message = "Product doesn't exist." });
            var result = await _productService.RemoveProductFromWishlistAsync(productId);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Remove product from wishlist failed. Server Error." });
        }

        [HttpPost("add-to-cart-from-wishlist")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddtoCartFromWishlistFromWishlistAsync()
        {
            var result = await _productService.MoveProFromWishlistToShoppingCart();
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Move product from wishlist to shopping cart failed. Server Error." });
        }













    }
}
