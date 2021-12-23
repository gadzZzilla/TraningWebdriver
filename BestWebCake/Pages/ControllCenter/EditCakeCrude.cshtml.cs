namespace BestWebCake.Pages.ControllCenter
{
    using BestWebCake.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize(Roles="Admin, Manager")]
    public class EditCakeCrudeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditCakeCrudeModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public CakeCrudeEntity CakeCrude { get; set; }
        [BindProperty]
        public CakeCrudeEntity Name { get; set; } 
    }
}