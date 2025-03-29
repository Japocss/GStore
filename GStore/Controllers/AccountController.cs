using System.Net.Mail;
using System.Security.Claims;
using GStore.Models;
using GStore.ViewModels
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GStore.Controllers
    public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly UseManager<Usuario> _userManager;
    private readonly IWebHostEnvironment _host;

    public AccountController(
        ILogger<AccountController> logger,
        SignInManager<Usuario> signInManager,
        UserManager<Usuario> userManager,
        IWebHostEnvironment
    )
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
        _host = host;
    }  

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        LoginVM login = new()
        {
            UrlRetorno = returnUrl ?? Url.Content("~/")
        };
        return View(login);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM login)
}