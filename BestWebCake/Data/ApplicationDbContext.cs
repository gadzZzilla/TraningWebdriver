namespace BestWebCake.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class ApplicationDbContext : IdentityDbContext
    {
        private DbContextOptions<ApplicationDbContext> Options;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Options = options;
        }

        public DbSet<CakeEntity> CakeDB { get; set; }
        public DbSet<UnitEntity> UnitDB { get; set; }
        public DbSet<CrudeEntity> CrudeDB { get; set; }
        public DbSet<CakeCrudeEntity> CakeCrudeDB { get; set; }
        public DbSet<IdentityUser> ApplicationUserDB { get; set; }
        public DbSet<IdentityRole> ApplicationRoleDB { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region CakeCrudeModel
            builder.Entity<CakeCrudeEntity>()
                .HasKey(bc => new { bc.Id });
            builder.Entity<CakeCrudeEntity>()
                .HasOne(c => c.CakeEntity)
                .WithMany(b => b.CakeCrudeEntity)
                .HasForeignKey(bc => bc.CakeId);

            builder.Entity<CakeCrudeEntity>()
                .HasOne(b => b.CrudeEntity)
                .WithMany(c => c.CakeCrudeEntity)
                .HasForeignKey(bc => bc.CrudeId);

            builder.Entity<CakeCrudeEntity>()
                .HasOne(c => c.UnitEntity)
                .WithMany(b => b.CakeCrudeEntity)
                .HasForeignKey(bc => bc.UnitId);
            #endregion

            #region UnitModel
            // builder.Entity<CrudeEntity>()
            //     .HasOne(p => p.UnitEntity)
            //     .WithMany(b => b.CrudeEntity)
            //     .HasForeignKey(bp => bp.UnitId);

            // builder.Entity<CakeCrudeEntity>()
            //     .HasOne(bc => bc.CrudeEntity)
            //     .WithMany(c => c.CakeCrudeEntity)
            //     .HasForeignKey(b => b.UnitId);
            #endregion
            // //OrderCake
            // builder.Entity<OrderCakeEntity>()
            //     .HasKey(bc => new { bc.OrderId, bc.CakeId });
            // builder.Entity<OrderCakeEntity>()
            //     .HasOne(bc => bc.OrderEntity)
            //     .WithMany(b => b.OrderCakes)
            //     .HasForeignKey(bc => bc.OrderId);

            // builder.Entity<OrderCakeEntity>()
            //     .HasOne(bc => bc.CakeEntity)
            //     .WithMany(c => c.OrderCakes)
            //     .HasForeignKey(bc => bc.CakeId);

            base.OnModelCreating(builder);
        }

        //public void CreateRoles()
        //{
        //    var adminRole = "Admin";
        //    var roleNames = new String[] { adminRole, "Manager", "Crew", "Guest", "Designer" };

        //    foreach (var roleName in roleNames)
        //    {
        //        var role = (roleName);
        //        if (!role)
        //        {
        //            var result = _rolesManager.CreateAsync(new IdentityRole { Name = roleName });

        //            //_logger.LogInformation("Create {0}: {1}", roleName, result.Succeeded);
        //        }
        //    }
        //    // administrator
        //    var user = new IdentityUser
        //    {
        //        UserName = Configuration["UserSettings:UserName"],
        //        Email = Configuration["UserSettings:UserEmail"],
        //        EmailConfirmed = true
        //    };
        //    string userPWD = Configuration["UserSettings:UserPassword"];
        //    var i = await _userManager.FindByEmailAsync(user.Email);
        //    if (i == null)
        //    {
        //        var adminUser = await _userManager.CreateAsync(user, userPWD);
        //        if (adminUser.Succeeded)
        //        {
        //            await _userManager.AddToRole(user, adminRole);

        //            _logger.LogInformation("Create {0}", user.UserName);
        //        }
        //    }
        //}
    }
}