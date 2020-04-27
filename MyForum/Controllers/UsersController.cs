using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyForum.Data;
using MyForum.ViewModels;

namespace MyForum.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public UsersController(UserManager<User> userManager, IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewBag.Genders = new SelectList(AccountController.genders, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email, UserName = model.Email, FirstName = model.FirstName, SecondName = model.SecondName,
                    BirthDate = model.BirthDate, RegisterDate = DateTime.Now, GenderId = model.IdGender, EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);
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
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Genders = new SelectList(AccountController.genders, "Id", "Name");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, BirthDate = user.BirthDate, 
                FirstName = user.FirstName, SecondName = user.SecondName, IdGender = user.GenderId, Country = user.Country,
                City = user.City, AboutMe = user.AboutMe, NumberPhone = user.NumberPhone
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.BirthDate = model.BirthDate;
                    user.FirstName = model.FirstName;
                    user.SecondName = model.SecondName;
                    user.Country = model.Country;
                    user.City = model.City;
                    user.PhoneNumber = model.NumberPhone;
                    user.AboutMe = model.AboutMe;

                    var result = await _userManager.UpdateAsync(user);
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
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
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
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult UserProfile(string userId)
        {
            if(userId == null) { 
                ViewBag.NameUser = User.Identity.Name;
                return View(_userManager.Users.ToList().Where(p => User.Identity.Name == p.UserName));
            }
            else
            {
                return View(_userManager.Users.ToList().Where(p => userId == p.Id));
            }
        }

        [HttpPost]
        public async Task<RedirectToActionResult> AddPhoto(IFormFile uploadedFile)
        {

            User user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (uploadedFile != null)
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + @"\img\" + uploadedFile.FileName, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }

                user.PhotoName = uploadedFile.FileName;
                await _userManager.UpdateAsync(user);
              //  await _db.SaveChangesAsync().ConfigureAwait(true);

               
            }
            return RedirectToAction("UserProfile", "Users");
        }



    }
}
