namespace BestWebCake.Pages.ControllCenter
{
    using BestWebCake.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    [Authorize(Roles="Admin, Manager")]
    public class CreateCakeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateCakeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CakeEntity Cake { get; set; }

        public IList<CakeCrudeEntity> CakeCrude { get; private set; }

        public IFormFile Image {get;set;}

        public async Task<IActionResult> OnPostAsync(IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _db.SaveChangesAsync();
            return RedirectToPage("./Cake");
        }
    }
}