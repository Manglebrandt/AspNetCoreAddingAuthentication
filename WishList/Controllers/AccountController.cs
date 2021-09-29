using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

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

		[HttpGet, AllowAnonymous]
		public IActionResult Login()
		{
			return View("Login");
		}

		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		public IActionResult Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Login", model);
			}

			var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false).Result;
			if (!result.Succeeded)
			{
				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				return View("Login", model);
			}

			return RedirectToAction("Index", "Item");
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Logout()
		{
			_signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
