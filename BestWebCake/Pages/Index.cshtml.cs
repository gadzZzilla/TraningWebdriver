﻿namespace BestWebCake.Pages
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
