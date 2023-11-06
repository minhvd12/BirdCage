using BirdCageShopInterface.IRepositories;
using BirdCageShopReposiory.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
      

        [HttpGet]
        public async Task<IActionResult> TestGEt()
        {
            Response.Headers.Add("Location", "https://checkout.stripe.com/c/pay/cs_test_a12fQxQB5lkuFNAyU3rB3H49Y84qD5EiagWkn38i0W37jOC8YK7OFCDOIv#fidkdWxOYHwnPyd1blpxYHZxWjA0SjRVVTVPaGAyanJUUUJDSEJzTlYwZ1BQUFBcSXNockxNcFZjU2dDR1UyN2NUVk9DN1VMPGBubDM3SWExc2xqMjx8SF1uVH1iYD1GMD1ydWNAVDNOc0RpNTU0bEpwTWdKaScpJ2N3amhWYHdzYHcnP3F3cGApJ2lkfGpwcVF8dWAnPyd2bGtiaWBabHFgaCcpJ2BrZGdpYFVpZGZgbWppYWB3dic%2FcXdwYHgl");
            return new StatusCodeResult(303);
        }
    }
}
