using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using BestWebCake.Data;

namespace BestWebCake.Pages.ControllCenter
{
    [Authorize(Roles="Admin, Manager")]
    public class CakeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CakeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<CakeEntity> Cakes { get; private set; }

        public async Task OnGetAsync()
        {
            Cakes = await _db.CakeDB.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var cake = await _db.CakeDB.FindAsync(id);

            if (cake != null)
            {
                _db.CakeDB.Remove(cake);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}