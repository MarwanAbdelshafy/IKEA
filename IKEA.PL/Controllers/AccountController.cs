using System.Threading.Tasks;
using IKEA.DAL.Models.Identity;
using IKEA.PL.ViewModels.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Services
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        #endregion



         #region SignUP
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var User =await userManager.FindByNameAsync(signUpViewModel.UserName);

            if (User is not null)
            {
                ModelState.AddModelError(nameof( signUpViewModel.UserName), "This UserName is already taken");
                return View(signUpViewModel);
            }
            User = new ApplicationUser()
            {
                FirstName = signUpViewModel.FirstName,
                LastName = signUpViewModel.LastName,
                UserName = signUpViewModel.UserName,
                Email = signUpViewModel.Email,
                IsAgreed = signUpViewModel.IsAgree
            };
            var Result = await userManager.CreateAsync(User, signUpViewModel.Password);

            if (Result.Succeeded)
                return RedirectToAction(nameof(LogIn));


            foreach (var error in Result.Errors)
                ModelState.AddModelError(string.Empty,error.Description);




            return View(signUpViewModel);


        }
        #endregion

        #region LogIn
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();



            var User = await userManager.FindByEmailAsync(logInViewModel.Email);
            if (User is not null)
            {
                var flag = await signInManager.PasswordSignInAsync(User, logInViewModel.Password, logInViewModel.RememberMe, true);
                if (flag.IsNotAllowed)
                    ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed");

                if (flag.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Your Account Is Locked Out");

                if (flag.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index), "Home");


            }
            ModelState.AddModelError(string.Empty, "Invalid LogIn");
            return View(logInViewModel);





            var result = await userManager.CheckPasswordAsync(User, logInViewModel.Password);
            if (!result)
            {
                return View(logInViewModel);
            }
            return RedirectToAction(nameof(Index), "Home");
        }


        #endregion


        #region SignOut

        public async Task<ActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));

        }
        #endregion
    }
}
