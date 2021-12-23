namespace BestWebCake.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using BestWebCake.Data;


    [Route("[controller]/[action]")]
    public class CakeController : BaseController
    {
        public CakeController(SignInManager<IdentityUser> signInManager,
          ILogger<AccountController> logger,
          ApplicationDbContext db)
        :base(signInManager,logger,db)
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CakeAddImage(CakeEntity Cake, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            byte[] dataArray = new byte[size];
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        Cake.ContentType = formFile.ContentType;
                        Cake.Image = memoryStream.ToArray();
                    }
                }
            }
            _db.CakeDB.Add(Cake);
            await _db.SaveChangesAsync();
            return RedirectToPage("/ControllCenter/Cake");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCake(CakeEntity Cake, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            byte[] dataArray = new byte[size];
            if(files.Count==0)
            {
                Cake.Image = _db.CakeDB.Find(Cake.Id).Image;
            }
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        Cake.ContentType = formFile.ContentType;
                        Cake.Image = memoryStream.ToArray();
                    }
                }
            }
            var result = _db.CakeDB.SingleOrDefault(b =>b.Id == Cake.Id);
            result.Description = Cake.Description;
            result.Name = Cake.Name;
            result.Price = Cake.Price;
            result.Weight = Cake.Weight;
            result.Image = Cake.Image;
            await _db.SaveChangesAsync();
            return RedirectToPage("/ControllCenter/Cake");
        }

        public async Task<IActionResult> DeletePicCake(CakeEntity Cake)
        {
            var result = _db.CakeDB.SingleOrDefault(b=>b.Id == Cake.Id);
            result.Image = null;
            await _db.SaveChangesAsync();
            return RedirectToPage("/ControllCenter/Cake");
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult GetImage(CakeEntity Cake)
        {
            byte[] imageByteData = Cake.Image;
            return File(imageByteData, "image/png");
        }
        //UPDATE 2016-06-07:
        //Some Browsers like IE do not seem to accept the ContentType parameter of File() and change it to GIF, which may result in lower image quality!
        // //Better use this code:
        // public ActionResult GetImage()
        // {
        // System.IO.MemoryStream ms = new System.IO.MemoryStream();
        // yourImage.Save(ms, resultImageType); //or use your Byte[] directly with Memorystream constructor
        // ms.Position = 0;
        // return new FileStreamResult(ms, "image/png");
        // }
    }

}