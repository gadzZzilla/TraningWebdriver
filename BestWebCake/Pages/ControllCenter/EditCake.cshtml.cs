namespace BestWebCake.Pages.ControllCenter
{
    using BestWebCake.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    [Authorize(Roles="Admin, Manager")]
    public class EditCakeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditCakeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // public IList<CakeCrudeEntity> CakeCrude { get; private set; }

        [BindProperty]
        public CakeEntity Cake { get; set; }

        [BindProperty]
        public double Weight { get; set; }

        [BindProperty]
        public IFormFile Image {get;set;}

        [BindProperty]
        public double Price { get; set; }
        
        [BindProperty]
        public String img64Url {get;set;}

        public async Task<IActionResult> OnGetAsync(Guid id, string name)
        {
            Cake = await _db.CakeDB.FindAsync(id);

            if (Cake == null)
            {
                return RedirectToPage("./Cake");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                Cake = await _db.CakeDB.FindAsync(id);
                var CakeCrude = await _db.CakeCrudeDB.AsNoTracking().ToListAsync();
                if(Cake.Image!=null)
                {
                    String img64 = Convert.ToBase64String(Cake.Image);
                    img64Url = string.Format("data:"+ Cake.ContentType +";base64,{0}", img64);
                }
                return Page();
            }
            Price = Cake.Price;
            Weight = Cake.Weight;
            _db.Attach(Cake).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"Cake {Cake.Id} not found!");
            }

            return RedirectToPage("./Cake");
        }
    }
}