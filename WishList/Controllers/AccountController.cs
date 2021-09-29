using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;

namespace WishList.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet, AllowAnonymous]
		public IActionResult Register()
		{
			return View("Register");
		}

		[HttpPost, AllowAnonymous]
		public IActionResult Register(Models.AccountViewModels.RegisterViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View("Register", viewModel);
			}


			var result = _userManager
				.CreateAsync(new ApplicationUser() { Email = viewModel.Email, UserName = viewModel.Email },
					viewModel.Password).Result;

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("Password", error.Description);
				}

				return View(viewModel);
			}

			return RedirectToAction("Index", "Home");
		}
	}
}
