using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeHouse.Areas.Admin.Models;
using CoffeeHouse.Data.Models;
using CoffeeHouse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            ListIdentityUser list = new ListIdentityUser
            {
                ListUser = new List<ListUserWithRole>()
            };

            List<IdentityUser> identityUsers = await _userManager.Users.ToListAsync();
            foreach (var user in identityUsers)
            {

                list.ListUser.Add(new ListUserWithRole()
                {
                    User = user,
                    RoleName = (await _userManager.GetRolesAsync(user))[0].ToString()
                });
            }



            return View(list);
        }

        public async Task<IActionResult> Detail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userVM = new EditViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return View(userVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model, string email)
        {
            var editUser = await _userManager.FindByEmailAsync(email);
            var oldRoles = await _userManager.GetRolesAsync(editUser);
            if (ModelState.IsValid)
            {
                editUser.PhoneNumber = model.PhoneNumber;
                await _userManager.UpdateAsync(editUser);
                await _userManager.RemoveFromRolesAsync(editUser, oldRoles);
                await AddRoleUser(model.Type, editUser);
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByEmailAsync(email);
            var userVM = new EditViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(userVM);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var rolesForUser = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, rolesForUser);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a newaccount with password.");
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation("User created a newaccount with password.");
                    //await AddRoleUser(RoleType.User, user);
                    await AddRoleUser(model.Type, user);
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private async Task AddRoleUser(RoleType type, IdentityUser user)
        {
            switch (type)
            {
                case RoleType.Admin:
                    await _userManager.AddToRoleAsync(user, "Admin ");
                    break;
                case RoleType.User:
                    await _userManager.AddToRoleAsync(user, "User");
                    break;
                case RoleType.Staff:
                    await _userManager.AddToRoleAsync(user, "Seller");
                    break;
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(AccountController.Index), "Account");
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}