using BrainUp.Data;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BrainUp.Models;
using Task = System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Authorization;
using BrainUp.StoredProcedure;

namespace BrainUp.Controllers
{
    public class AccountController : Controller
    {
        private readonly BrainUpBdContext db;
        private readonly IWebHostEnvironment _environment;

        public AccountController(BrainUpBdContext context, IWebHostEnvironment environment)
        {
            db = context;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    if (user.Role.Name == "Admin")
                    {
                        return Redirect("/Admin/Index/");
                    }

                    return RedirectToAction("Index","Cources");
                }

                ModelState.AddModelError("", "Incorrect login or password");

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    var role = await db.Roles.FirstOrDefaultAsync(r => r.Name == "Student");
                    user = new User { Email = model.Email, PasswordHash = model.Password, RoleId = role.Id, Role = role };
                    db.Users.Add(user);
                    await db.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterTeacher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterTeacher(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    var role = await db.Roles.FirstOrDefaultAsync(r => r.Name == "Teacher");
                    user = new User { Email = model.Email, PasswordHash = model.Password, RoleId = role.Id, Role = role };
                    db.Users.Add(user);
                    await db.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Cources");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userModel = new UserViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

                userModel.Id = user.Id;
                userModel.Email = user.Email;
                userModel.FirstName = user.FirstName;
                userModel.LastName = user.LastName;
                userModel.PhoneNumber = user.PhoneNumber;
                userModel.Discription = user.Discription;
                userModel.ImageString = user.Image;

                double point = PointAction.GetAllPointsForUser(user.Id, db);

                ViewData["Point"] = point;
            }

            return View(userModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Discription = model.Discription;

                    if (model.ImageFormFile != null)
                    {
                        var fileName = $"{model.Email.Replace(" ", "")}" + Path.GetExtension(model.ImageFormFile.FileName);
                        user.Image = fileName;
                        var path = Path.Combine(_environment.WebRootPath, "img", fileName);
                        using (var fStream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFormFile.CopyToAsync(fStream);
                        }
                    }

                    db.Users.Update(user);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index", "Cources");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }

            return View(model);
        }

        private async System.Threading.Tasks.Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
