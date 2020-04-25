using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qwer.Models;
using qwer.ViewModels;

namespace qwer.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated && !((await _userManager.FindByNameAsync(User.Identity.Name)).BlockedStatus))
            {
                return View(_userManager.Users.ToList());
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUsers(string[] selected)
        {
            var targetUsers = _userManager.Users.Where(user => selected.Contains(user.Id)).ToList();

            if (selected.Contains((await _userManager.FindByNameAsync(User.Identity.Name)).Id))
            {
                await _signInManager.SignOutAsync();

                foreach (var item in targetUsers)
                {
                    await _userManager.DeleteAsync(item);
                }

                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            else
            {
                foreach (var item in targetUsers)
                {
                    await _userManager.DeleteAsync(item);
                }
            }

            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> BlockUsers(string[] selected)
        {
            var usersToUpdate = _userManager.Users.Where(user => selected.Contains(user.Id)).ToList();
            if (selected.Contains((await _userManager.FindByNameAsync(User.Identity.Name)).Id))
            {
                await _signInManager.SignOutAsync();

                foreach (var item in usersToUpdate)
                {
                    item.BlockedStatus = true;
                    await _userManager.UpdateAsync(item);
                }

                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            else
            {
                foreach (var item in usersToUpdate)
                {
                    item.BlockedStatus = true;
                    await _userManager.UpdateAsync(item);
                }
            }

            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUsers(string[] selected)
        {
            var usersToUpdate = _userManager.Users.Where(user => selected.Contains(user.Id)).ToList();
            foreach (var item in usersToUpdate)
            {
                item.BlockedStatus = false;
                await _userManager.UpdateAsync(item);
            }

            return Redirect("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }
    }
}