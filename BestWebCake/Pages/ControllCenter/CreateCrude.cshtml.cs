namespace BestWebCake.Pages.ControllCenter
{
    using BestWebCake.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [Authorize(Roles="Admin, Manager")]
    public class CreateCrudeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateCrudeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CrudeEntity Crude { get; set; }

        [Required]
        [BindProperty]
        public UnitEntity Unit { get; set; }

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

        // public async Task<IActionResult> OnPostAsync()
        // {
        //     if(SelectedUnit != Guid.Empty)
        //     {
        //         Crude.UnitId = _db.UnitDB.Find(SelectedUnit).Id;
        //         _db.CrudeDB.Add(Crude);
        //         await _db.SaveChangesAsync();
        //     }
        //     if (!ModelState.IsValid)
        //     {
        //         return Page();
        //     }
        //     _db.CrudeDB.Add(Crude);
        //     await _db.SaveChangesAsync();
        //     return RedirectToPage("./Crude");
        // }

        // public async Task OnGetAsync(Guid id)
        // {
        //     Crude = await _db.CrudeDB.FindAsync(id);
        // }
    }
}


            // Cake = _db.CakeDB.Find(id);
            // Crude = _db.CrudeDB.Find(SelectedCrude);
            // _db.CakeCrudeDB.Add(new Entity.CakeCrudeEntity()
            // {
            //     CakeId = Cake.Id,
            //     CrudeId = Crude.Id,
            //     CrudeName = Crude.Name,
            //     WeightCrude = this.weightCrude
            // });

            // await _db.SaveChangesAsync();