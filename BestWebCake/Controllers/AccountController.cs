namespace BestWebCake.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;



    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        public AccountController(SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        : base(signInManager,logger)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }
    }
}
