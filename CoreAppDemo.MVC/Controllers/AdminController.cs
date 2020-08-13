using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Application.ViewModels;
using CoreApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreAppDemo.MVC.Controllers
{
	
	public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
        {
            return View();
        }

		[Authorize(Policy = "createpolicy")]
		public IActionResult GetRoles()
		{
			var roles = _roleManager.Roles.ToList();
			return View(roles);
		}

		[Authorize(Policy = "createpolicy")]
		public IActionResult CreateRole()
		{
			return View(new IdentityRole());
		}

		[Authorize(Policy = "createpolicy")]
		[HttpPost]
		public async Task<IActionResult> CreateRole(IdentityRole role)
		{
			await _roleManager.CreateAsync(role);
			return RedirectToAction("GetRoles");
		}

		[Authorize(Policy = "createpolicy")]
		public async Task<IActionResult> Users()
		{
			var userViewModel = new List<UserViewModel>();

			try
			{
				var users = await _userManager.Users.ToListAsync();
				foreach (var item in users)
				{
					userViewModel.Add(new UserViewModel()
					{
						Id = item.Id,
						Email = item.Email,
						UserName = item.UserName,
					});
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return View(userViewModel);
		}

		[Authorize(Policy = "writepolicy")]
		public async Task<IActionResult> EditUser(string id)
		{
			var viewModel = new UserViewModel();
			if (!string.IsNullOrWhiteSpace(id))
			{
				var user = await _userManager.FindByIdAsync(id);
				var userRoles = await _userManager.GetRolesAsync(user);
				if (userRoles.Count > 0)
				{
					var Role = await _roleManager.FindByNameAsync(userRoles[0]);
					viewModel.RoleName = Role.Name;
				}
				else
				{
					viewModel.RoleName = null;
				}
			
				viewModel.Email = user?.Email;
				viewModel.UserName = user?.UserName;

				var allRoles = await _roleManager.Roles.ToListAsync();

				SelectList roleList = new SelectList((from data in allRoles
							   select data).ToList().Select(x =>
								new SelectListItem()
								{
									Text = x.Name.ToString(),
									Value = x.Name.ToString()
								}).ToList(), "Value", "Text");
				ViewBag.roleList = roleList;			
			}
			return View(viewModel);
		}


		[Authorize(Policy = "writepolicy")]
		[HttpPost]
		public async Task<IActionResult> EditUser(UserViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(viewModel.Id);
				var userRoles = await _userManager.GetRolesAsync(user);

				await _userManager.RemoveFromRolesAsync(user, userRoles);
				await _userManager.AddToRoleAsync(user,viewModel.RoleName);

				return RedirectToAction(nameof(Users));
			}

			return View(viewModel);
		}


	}
}