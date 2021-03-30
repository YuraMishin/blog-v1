using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Email;
using Web.ViewModels;

namespace Web.Controllers
{
  /// <summary>
  /// Сlass AuthController
  /// </summary>
  public class AuthController : Controller
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private IEmailService _emailService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="signInManager">signInManager</param>
    /// <param name="userManager">userManager</param>
    /// <param name="emailService">emailService</param>
    public AuthController(
      SignInManager<IdentityUser> signInManager,
      UserManager<IdentityUser> userManager,
      IEmailService emailService)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _emailService = emailService;
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
      if (!ModelState.IsValid)
      {
        return RedirectToAction("Index", "Home");
      }

      var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);

      if (!result.Succeeded)
      {
        return View(loginVM);
      }

      var user = await _userManager.FindByNameAsync(loginVM.UserName);

      var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

      if (isAdmin)
      {
        return RedirectToAction("Index", "Panel");
      }

      return RedirectToAction("Index", "Home");
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

    /// <summary>
    /// Method displays register UI
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    public IActionResult Register()
    {
      return View(new RegisterViewModel());
    }

    /// <summary>
    /// Method handles user registration.
    /// POST: /auth/register
    /// </summary>
    /// <param name="registerVM">registerVM</param>
    /// <returns>Task&lt;IActionResult&gt;</returns>
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerVM)
    {
      if (!ModelState.IsValid)
      {
        return View(registerVM);
      }

      var user = new IdentityUser
      {
        UserName = registerVM.Email,
        Email = registerVM.Email
      };

      var result = await _userManager.CreateAsync(user, "password");

      if (result.Succeeded)
      {
        await _signInManager.SignInAsync(user, false);
        await _emailService.SendEmail(user.Email, "Welcome", "Thank you for registering!");
        return RedirectToAction("Index", "Home");
      }

      return View(registerVM);
    }
  }
}
