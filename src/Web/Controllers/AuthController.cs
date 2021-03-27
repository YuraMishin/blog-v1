using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
  /// <summary>
  /// Сlass AuthController
  /// </summary>
  public class AuthController : Controller
  {
    private readonly SignInManager<IdentityUser> _signInManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="signInManager">signInManager</param>
    public AuthController(SignInManager<IdentityUser> signInManager)
    {
      _signInManager = signInManager;
    }

    /// <summary>
    /// Method displays login UI.
    /// GET: /auth/login
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    public IActionResult Login()
    {
      return View(new LoginViewModel());
    }

    /// <summary>
    /// Method handles login.
    /// POST: /auth/login
    /// </summary>
    /// <param name="loginVM">loginVM</param>
    /// <returns>Task&lt;IActionResult&gt;</returns>
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
      var result = await _signInManager.PasswordSignInAsync(
        loginVM.UserName,
        loginVM.Password,
        false,
        false);

      return RedirectToAction("Index", "Panel");
    }

    /// <summary>
    /// Method handles logout.
    /// GET: /auth/logout
    /// </summary>
    /// <returns>Task&lt;IActionResult&gt;</returns>
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }
  }
}
