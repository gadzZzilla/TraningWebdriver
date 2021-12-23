using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using BestWebCake.Data;

namespace BestWebCake.Pages.ControllCenter
{
    [Authorize(Roles="Admin, Manager")]
    public class CakeAddCrudeModel : PageModel
    {
        private Guid id {get;set;}
        private readonly ApplicationDbContext _db;

        public CakeAddCrudeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CrudeEntity Crude { get; set; }

        [BindProperty]
        public CakeEntity Cake { get; set; }

        [BindProperty]
        public double WeightCrude {get;set;}

        [BindProperty]
        public Guid SelectedCrude {get;set;}

        [BindProperty]
        public Guid SelectedCake {get;set;}

        public IList<CrudeEntity> Crudes
        {
            get
            {
                var list = new List<CrudeEntity>();

                foreach(var crude in  _db.Set<CrudeEntity>())
                {
                    list.Add(crude);
                }

                return list;
            }
        }
        public IList<CakeEntity> Cakes
        {
            get
            {
                var list = new List<CakeEntity>();

                foreach(var cake in  _db.Set<CakeEntity>())
                {
                    list.Add(cake);
                }

                return list;
            }
        }

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
        public IList<CakeCrudeEntity> CakeCrudes {
                        get
            {
                var list = new List<CakeCrudeEntity>();
                foreach(var tmp in  _db.Set<CakeCrudeEntity>())
                {
                    list.Add(tmp);
                }
                return list;
            }
          }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                Cake = await _db.CakeDB.FindAsync(id);
                // CakeCrudes = await _db.CakeCrudeDB.AsNoTracking().ToListAsync();
                return Page();
            }
            return Page();
        }

        public async Task OnGetAsync(Guid id)
        {
            Cake = await _db.CakeDB.FindAsync(id);

            // CakeCrudes = await _db.CakeCrudeDB.AsNoTracking().ToListAsync();
        }
    }
}