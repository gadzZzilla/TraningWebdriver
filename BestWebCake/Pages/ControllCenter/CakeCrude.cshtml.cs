namespace BestWebCake.Pages.ControllCenter
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;
    using BestWebCake.Data;

    [Authorize(Roles="Admin, Manager")]
    public class CakeCrudeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CakeCrudeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CrudeEntity Crude { get; set; }

        [BindProperty]
        public CakeEntity Cake { get; set; }

        public ICollection<CakeCrudeEntity> CakeCrudes { get; private set; }

        public async Task OnGetAsync()
        {
            CakeCrudes = await _db.CakeCrudeDB.AsNoTracking().ToListAsync();
        }

        public double Price()
        {
            return 1;
        }
    }
}