using System.Linq;
using System.Threading.Tasks;
using Heavy.Web.Data;
using Heavy.Web.Models;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserAddViewModel userAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userAddViewModel);
            }

            var user = new ApplicationUser
            {
                UserName = userAddViewModel.UserName,
                Email = userAddViewModel.Email,
                IdCardNo = userAddViewModel.IdCardNo,
                BirthDate = userAddViewModel.BirthDate
            };

            var result = await _userManager.CreateAsync(user, userAddViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(userAddViewModel);

        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var claims = await _userManager.GetClaimsAsync(user);

            var vm = new UserEditViewModel
            {
                Id = user.Id,
                BirthDate = user.BirthDate,
                Email = user.Email,
                IdCardNo = user.IdCardNo,
                UserName = user.UserName,
                Claims = claims.Select(x => x.Value).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, UserEditViewModel userEditViewModel)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            user.UserName = userEditViewModel.UserName;
            user.Email = userEditViewModel.Email;
            user.IdCardNo = userEditViewModel.IdCardNo;
            user.BirthDate = userEditViewModel.BirthDate;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "更新用户信息时发生错误");
            return View(userEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "删除用户时发生错误");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "用户找不到");
            }

            return View("Index", await _userManager.Users.ToListAsync());
        }

        public async Task<IActionResult> ManageClaims(string id)
        {
            var user = await _userManager.Users.Include(x => x.Claims)
                .Where(x => x.Id == id).SingleOrDefaultAsync();
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var leftClaims = ClaimTypes.AllClaimTypeList.Except(user.Claims.Select(x => x.ClaimType)).ToList();

            var vm = new ManageClaimsViewModel
            {
                UserId = id,
                AvailableClaims = leftClaims
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ManageClaims(ManageClaimsViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.UserId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var claim = new IdentityUserClaim<string>
            {
                ClaimType = vm.ClaimId,
                ClaimValue = vm.ClaimId
            };

            user.Claims.Add(claim);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("EditUser", new { id = vm.UserId });
            }

            ModelState.AddModelError(string.Empty, "编辑用户Claims时发生错误");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveClaim(string id, string claim)
        {
            var user = await _userManager.Users.Include(x => x.Claims)
                .Where(x => x.Id == id).SingleOrDefaultAsync();
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var claims = user.Claims.Where(x => x.ClaimType == claim).ToList();

            foreach (var c in claims)
            {
                user.Claims.Remove(c);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("EditUser", new { id });
            }

            ModelState.AddModelError(string.Empty, "编辑用户Claims时发生错误");
            return RedirectToAction("ManageClaims", new { id });
        }
    }
}
