namespace BestWebCake.Pages.ControllCenter
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BestWebCake.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles="Admin, Manager")]
    public class EditCrudeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditCrudeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CrudeEntity Crude { get; set; }

        [BindProperty]
        public Guid SelectedUnit { get; set; }

        public IList<UnitEntity> Units
        {
            get
            {
                var list = new List<UnitEntity>();
                foreach(var unit in  _db.Set<UnitEntity>())
                {
                    list.Add(unit);
                }
                return list;
            }
        }

        public async Task<IActionResult> OnGetAsync(Guid id, string name)
        {
            Crude = await _db.CrudeDB.FindAsync(id);

            if (Crude == null)
            {
                return RedirectToPage("./Crude");
            }

            return Page();
        }

        public string UnitName(Guid id)
        {
            var resulte = string.Empty;
            if(id!=Guid.Empty)
            {
                resulte = _db.UnitDB.Find(id).ShortName;
            }
            return resulte;
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                Crude = await _db.CrudeDB.FindAsync(id);
                return Page();
            }
            Crude.UnitEntity = _db.UnitDB.Find(SelectedUnit);
            await _db.SaveChangesAsync();
            _db.Attach(Crude).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"Crude {Crude.Id} not found!");
            }

            return RedirectToPage("./Crude");
        }
    }
}