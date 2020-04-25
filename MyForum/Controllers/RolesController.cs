using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MyForum.Models;
using MyForum.ViewModels;
using MyForum.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace MyForum.Controllers
{
    public class RolesController : Controller
    {

        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index() => View(_roleManager.Roles.ToList());

        [HttpGet("create")]
        public IActionResult Create() => View();

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm]string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name)).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id).ConfigureAwait(true);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role).ConfigureAwait(true);
            }
            return RedirectToAction("Index");
        }
        [HttpGet("userList")]
        public IActionResult UserList() => View(_userManager.Users.ToList());

        [HttpGet("role")]
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(true);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(true);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("role")]

        public async Task<IActionResult> Edit([FromForm]string userId, [FromForm]List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(true);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(true);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles).ConfigureAwait(true);

                await _userManager.RemoveFromRolesAsync(user, removedRoles).ConfigureAwait(true);
                return RedirectToAction("Index", "Topics");
            }
            return NotFound();
        }

    }
}
