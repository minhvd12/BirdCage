using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rs = await _statusService.GetStatusAsync();
            return Ok(rs);
        }
    }
}
