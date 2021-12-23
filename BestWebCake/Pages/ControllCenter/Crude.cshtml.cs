namespace BestWebCake.Pages.ControllCenter
{
    using BestWebCake.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize(Roles="Admin, Manager")]
    public class CrudeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CrudeModel(ApplicationDbContext db)
        {
            _db = db;
        }

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

        public IList<CrudeEntity> Crudes
        {
            get
            {
                var list = new List<CrudeEntity>();
                foreach(var crude in  _db.Set<CrudeEntity>())
                {
                    var tmp = _db.UnitDB.Find(crude.UnitId);
                    crude.UnitEntity = tmp;
                    list.Add(crude);
                }
                return list;
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var crude = await _db.CrudeDB.FindAsync(id);

            if (crude != null)
            {
                _db.CrudeDB.Remove(crude);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}