using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopViewModel;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using BirdCageShopUtils;
using BirdCageShopDomain.Models;
using Stripe;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private static string s_wasmClientURL = string.Empty;

        public OrderController(IOrderService orderService, IConfiguration configuration)
        {
            _orderService = orderService;
            _configuration = configuration;
        }

        [HttpGet("filter-orderstatusId/{orderStatusId}")]
        public async Task<IActionResult> GetOrderByOderStatusPageAsync([FromRoute] int orderStatusId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _orderService.GetOrderByOderStatusPageAsync(orderStatusId, pageIndex, pageSize);
            return Ok(result);
        }



        [HttpGet("page")]

        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _orderService.GetPaginationAsync(pageIndex, pageSize);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        /// update status order to approved where : orderstatus : pending and payment status: payonline-approved or COD
        [HttpPut("updateStatusToApproved/{id}")]
        public async Task<IActionResult> updateStatusToApproved([FromRoute] int id)
        {

            bool isUpdate = await _orderService.GetByIdToUpdateStatusToApprovedAsync(id);
            if (isUpdate) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Update order status failed. Server Error." });
        }

        [HttpPut("updateStatusToShipped/{id}")]
        public async Task<IActionResult> UpdateOrderToShipped([FromRoute] int id)
        {

            bool isUpdate = await _orderService.GetByIdToUpdateStatusToShippeddAsync(id);
            if (isUpdate) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Update order status failed. Server Error." });
        }

        [HttpPut("cod-confirm-paid/{id}")]
        public async Task<IActionResult> UpdatePayStatusToAprroved([FromRoute] int id)
        {

            bool isUpdate = await _orderService.GetByIdToUpdateStatusPayToApprovedAsync(id);
            if (isUpdate) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Update payment status failed. Server Error." });
        }
        ///

     

        ///////
        //[HttpPost("Pay/{orderId}")]
        //public async Task<ActionResult> CheckoutOrder([FromRoute] int orderId, [FromServices] IServiceProvider sp)
        //{
        //    var order = await _orderService.GetOrderByIdAsync(orderId);
        //    if (!order.PaymentStatus.Equals("PayOnline")) return BadRequest();
        //    var referer = Request.Headers.Referer;
        //    s_wasmClientURL = referer[0];

        //    // Build the URL to which the customer will be redirected after paying.
        //    var server = sp.GetRequiredService<IServer>();

        //    var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

        //    string? thisApiUrl = null;

        //    if (serverAddressesFeature is not null)
        //    {
        //        thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
        //    }

        //    if (thisApiUrl is not null)
        //    {
        //        var sessionId = await CheckOut(order, thisApiUrl);
        //        var pubKey = _configuration["Stripe:PubKey"];

        //        var checkoutOrderResponse = new CheckoutOrderResponse()
        //        {
        //            SessionId = sessionId,
        //            PubKey = pubKey
        //        };

        //        return Ok(checkoutOrderResponse);
        //    }
        //    else
        //    {
        //        return StatusCode(500);
        //    }
        //}

        //[NonAction]
        //public async Task<string> CheckOut(Order order, string thisApiUrl)
        //{
        //    // Create a payment flow from the items in the cart.
        //    // Gets sent to Stripe API.
        //    var options = new SessionCreateOptions
        //    {
        //        // Stripe calls the URLs below when certain checkout events happen such as success and failure.
        //        SuccessUrl = $"{thisApiUrl}/order/success?sessionId=" + "{CHECKOUT_SESSION_ID}", // Customer paid.
        //        CancelUrl = s_wasmClientURL + "failed",  // Checkout cancelled.
        //        LineItems = new List<SessionLineItemOptions>(),
        //        Mode = "payment",
        //    };
        //    foreach (var item in order.Details)
        //    {
        //        var sessionLineItem = new SessionLineItemOptions
        //        {
        //            PriceData = new SessionLineItemPriceDataOptions
        //            {
        //                UnitAmount = (long)(item.Price * 100), // $20.50 => 2050
        //                Currency = "usd",
        //                ProductData = new SessionLineItemPriceDataProductDataOptions
        //                {
        //                    Name = item.Product.Title
        //                }
        //            },
        //            Quantity = item.Quantity
        //        };
        //        options.LineItems.Add(sessionLineItem);
        //    }

        //    var service = new SessionService();
        //    var session = await service.CreateAsync(options);

        //    return session.Id;
        //}

        ////  //update order information: order status != processing
        //[HttpPut("update-inform-order/{orderid}")]
        //public async Task<IActionResult> UpdateInformOrderAsync([FromRoute] int orderid)
        //{

        //}


        //[HttpGet("success")]
        //// Automatic query parameter handling from ASP.NET.
        //// Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
        //public ActionResult CheckoutSuccess(string sessionId)
        //{
        //    var sessionService = new SessionService();
        //    var session = sessionService.Get(sessionId);

        //    // Here you can save order and customer details to your database.
        //    var total = session.AmountTotal.Value;
        //    //var customerEmail = session.CustomerDetails.Email;
        //    if (session.PaymentStatus.ToLower() == "paid")
        //    {
        //        //_unitOfWork.OrderHeader.UpdateStripePaymentID(id, session.Id, session.PaymentIntentId);
        //        //_unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
        //        //_unitOfWork.Save();
        //        //_unit
        //    }
        //    return Redirect(s_wasmClientURL + "success");
        //}
    }
}

