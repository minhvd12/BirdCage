//using BirdCageShopInterface.IServices;
//using BirdCageShopViewModel.Auth;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BirdCageShop.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class AuthController : ControllerBase
//	{
//		private readonly IUserService _userService;
//		public AuthController(IUserService userService)
//		{
//			_userService = userService;
//		}

//		[HttpPost("authorize")]
//		public async Task<IActionResult> AuthorizeAsync([FromBody] SignInViewModel vm)
//		{
//			var result = await _userService.AuthorizeAsync(vm);
//			if (result is null) return Unauthorized(new { message = "Login Failed. Incorrect Email or Password." });
//			return Ok(result);
//		}

//	}
//}
