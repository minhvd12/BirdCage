using BirdCageShopInterface.IRepositories;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopViewModel.Voucher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVourcherService _vourcherService;
        public VoucherController(IVourcherService vourcherService)
        {
            _vourcherService = vourcherService;
        }



        //get all vourcher exp date > current date
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rs = await _vourcherService.GetVourcherAsync();
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] VourcherAddViewModel vm)
        {
            // check valid model
            var validateResult = await _vourcherService.ValidateVourcherAdddpAsync(vm);


            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
                return BadRequest(errors);
            }

            // add
            var result = await _vourcherService.CreateNewAsync(vm);

            if (result is true) return Created("/api/voucher", new { message = "Created Succeed." });
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Create Failed. Error Server." });
        }
    }
}
