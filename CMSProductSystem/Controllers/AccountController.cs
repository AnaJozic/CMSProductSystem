using CMSProductSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMSProductSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administratori")]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, Adresa = model.Adresa, Grad = model.Grad, Telefon = model.Telefon, UserName = model.Email, Email = model.Email, Zanimanje = model.Zanimanje, Aktivan = model.Aktivan, LastUpdated=DateTime.Now, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(model.Email);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administratori")]
        public IActionResult PopisUsera(int page)
        {
            var users = userManager.Users;

            double BrojRedaka = users.ToList().Count;
            int BrojStranica = (int)Math.Ceiling(BrojRedaka / 5);

            ViewBag.BrojStranica = BrojStranica;
            int aktivna;

            if (page == 0 || page == 1)
            {
                users = users.Take(5);
                aktivna = 1;
            }
            else
            {
                users = users.Skip((page - 1) * 5).Take(5);
                aktivna = page;
            }
            ViewBag.Aktivna = aktivna;


            return View(users);
        }
        [Authorize(Roles = "Administratori")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            EditUserViewModel model = new EditUserViewModel();
            model.Id = id;
            model.Email = user.Email;
            model.Zanimanje = user.Zanimanje;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Adresa = user.Adresa;
            model.Grad = user.Grad;
            model.Telefon = user.Telefon;
            model.Aktivan = user.Aktivan;
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Adresa = model.Adresa;
            user.Grad = model.Grad;
            user.Telefon = model.Telefon;
            user.Aktivan = model.Aktivan;
            user.Zanimanje = model.Zanimanje;
            user.LastUpdated = DateTime.Now;

            await userManager.UpdateAsync(user);
            return RedirectToAction("PopisUsera");
        }

        [Authorize(Roles = "Administratori")]
        public async Task<IActionResult> ViewUserDetails(string id)
        {
            List<string> listaRola = new List<string>();
            var user = await userManager.FindByIdAsync(id);
            EditUserViewModel model = new EditUserViewModel();
            model.Id = id;
            model.Email = user.Email;
            model.Zanimanje = user.Zanimanje;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Adresa = user.Adresa;
            model.Grad = user.Grad;
            model.Telefon = user.Telefon;
            model.Aktivan = user.Aktivan;
            model.PopisRola = await PopisRolaZaUsera(id);
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        public async Task<IActionResult> DeleteUserDetails(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            EditUserViewModel model = new EditUserViewModel();
            model.Id = id;
            model.Email = user.Email;
            model.Zanimanje = user.Zanimanje;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Adresa = user.Adresa;
            model.Grad = user.Grad;
            model.Telefon = user.Telefon;
            model.Aktivan = user.Aktivan;
            model.PopisRola = await PopisRolaZaUsera(id);
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        public async Task<IActionResult> DeleteUserConfirm(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var PopisRola = await PopisRolaZaUsera(id);
            foreach(var rola in PopisRola)
            {
                if (await userManager.IsInRoleAsync(user, rola))
                {
                    await userManager.RemoveFromRoleAsync(user, rola);
                }
            }
            
            await userManager.DeleteAsync(user);
            TempData["poruka"] = "Korisnik uspješno izbrisan";
            return RedirectToAction("PopisUsera");
        }

        [Authorize(Roles = "Administratori")]
        public IActionResult PopisRola()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [Authorize(Roles = "Administratori")]
        public IActionResult CreateRole()
        {
            CreateRoleViewModel model = new CreateRoleViewModel();
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("PopisRola", "Account");
                }

            }
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rola sa id = {id} ne postoji";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rola sa id-em = {model.Id} nije pronađena";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("PopisRola");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

        }

        [Authorize(Roles = "Administratori")]
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rola sa id-em = {roleId} nije pronađena";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rola sa id-em = {roleId} nije pronađena";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [Authorize(Roles = "Administratori")]
        public async Task<List<string>> PopisRolaZaUsera(string id)
        {

            List<string> roleUser = new List<string>();
            var roles = roleManager.Roles;
            var user = await userManager.FindByIdAsync(id);

            foreach (var role in roles)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    roleUser.Add(role.Name);
                }
            }
            return roleUser;
        }

    }
}
