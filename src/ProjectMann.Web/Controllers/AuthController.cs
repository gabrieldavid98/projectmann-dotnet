using Microsoft.AspNetCore.Mvc;
using ProjectMann.Infrastructure.Data;
using System.Threading.Tasks;
using ProjectMann.Web.Managers;
using ProjectMann.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProjectMann.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ProjectMannDbContext _db;

        private readonly IAuthManager _auth;
        
        public AuthController(ProjectMannDbContext db, IAuthManager auth)
        {
            _db = db;
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _auth.Validate(
                loginViewModel.Email, 
                loginViewModel.Password
            );

            if (user == null)
            {
                loginViewModel.IsBadLogin = true;
                ModelState.AddModelError("IsBadLogin", "E-mail o contraseña incorrectos");
                return View(loginViewModel);
            }

            if (user.Estado == false)
            {
                loginViewModel.IsBadLogin = true;
                ModelState.AddModelError("IsBadLogin", "El usuario esta inactivo");
                return View(loginViewModel);
            }

            await _auth.SignIn(HttpContext, user);

            return RedirectToAction(
                actionName: "Index",
                controllerName: "Home"
            );
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _auth.SignOut(HttpContext);
            }

            return RedirectToAction(
                controllerName: "Auth",
                actionName: "Login"
            );
        }
    }
}