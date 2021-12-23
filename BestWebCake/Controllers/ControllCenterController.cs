namespace BestWebCake.Controllers
{
    using BestWebCake.Data;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    [Route("[controller]/[action]")]
    public class ControllCenterController : BaseController
    {
        [BindProperty]
        public CrudeEntity Crude { get; set; }

        [BindProperty]
        public CakeEntity Cake { get; set; }

        [BindProperty]
        public UnitEntity Unit { get; set; }

        [BindProperty]
        public Guid SelectedCrude {get;set;}

        [BindProperty]
        public Guid SelectedUnit {get;set;}

        [BindProperty]
        public double weightCrude{get;set;}

        public ControllCenterController(SignInManager<IdentityUser> signInManager,
          ILogger<AccountController> logger,
          ApplicationDbContext db)
        :base(signInManager,logger,db)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCrude(Guid id)
        {
            Cake = _db.CakeDB.Find(id);
            Crude = _db.CrudeDB.Find(SelectedCrude);
            Crude.UnitEntity = _db.UnitDB.Find(SelectedUnit);
            Unit = _db.UnitDB.Find(SelectedUnit);
            var d = _db.CakeCrudeDB.Add(new CakeCrudeEntity()
            {
                CakeId = Cake.Id,
                CakeEntity = Cake,
                CrudeEntity = Crude,
                CrudeId = Crude.Id,
                UnitEntity = Unit,
                UnitId = Unit.Id,
                WeightCrude = weightCrude
            });

            await _db.SaveChangesAsync();
            return Redirect("CakeAddCrude/"+id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewCrude(Guid SelectedUnit)
        {
            Crude.UnitEntity = _db.UnitDB.Find(SelectedUnit);
            Crude.UnitId = Crude.UnitEntity.Id;
            if(Crude.UnitEntity.Weight>=1)
            {
                Crude.PriceOfOnce = Crude.Price / Crude.UnitEntity.Weight;
            }
            else{
                Crude.PriceOfOnce = Crude.Price * Crude.UnitEntity.Weight;
            }
            
            _db.CrudeDB.Add(Crude);
            await _db.SaveChangesAsync();
            return Redirect("Crude");
        }


        public async Task<IActionResult> Calculate(Guid id)
        {
            var Cake = _db.CakeDB.Find(id);
            var CakeCrudes = await _db.CakeCrudeDB.AsNoTracking().ToListAsync();
            double TotalPrice = 0;
            foreach(var item in CakeCrudes)
            {
                if(item.CakeId==id)
                {
                    var priceOfOnce = _db.CrudeDB.Find(item.CrudeId).PriceOfOnce;
                    var measure = _db.UnitDB.Find(item.UnitId);
                    var tmp = measure.Weight;
                    TotalPrice += item.WeightCrude/tmp * priceOfOnce;
                    // TotalPrice += item.WeightCrude * priceOfOnce;
                }
            }
            _db.Attach(Cake).State = EntityState.Modified;
            Cake.Price = TotalPrice;
            await _db.SaveChangesAsync();
            return Redirect("Cake");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var cakeCrude = _db.CakeCrudeDB.Find(id);
            if (cakeCrude != null)
            {
                _db.CakeCrudeDB.Remove(cakeCrude);
                _db.SaveChanges();
            }
            return Redirect("CakeAddCrude/" + cakeCrude.CakeId);
        }
    }
}