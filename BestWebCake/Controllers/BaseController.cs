namespace BestWebCake.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using BestWebCake.Data;

    [Route("[controller]/[action]")]
    public class BaseController : Controller
    {
        protected readonly SignInManager<IdentityUser> _signInManager;

        protected readonly ILogger _logger;

        protected readonly ApplicationDbContext _db;

        public BaseController(SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        public BaseController(SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger, ApplicationDbContext db)
        {
            _signInManager = signInManager;
            _logger = logger;
            _db = db;
        }
    }
}