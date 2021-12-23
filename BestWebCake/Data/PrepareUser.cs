namespace BestWebCake.Data
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;


    ///Клас создания суперадмина
    public class PrepareUser
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolesManager;

        public PrepareUser(IServiceProvider serviceProvider, ApplicationDbContext context,ILogger logger) {
            _context = context;
            _logger = logger;
            _rolesManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        }
        public static IConfiguration Configuration { get; set; }
        public async Task CreateRoles()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();


            if (await _context.Roles.AnyAsync())
            {
                // not waste time
                _logger.LogInformation("Exists Roles.");
                return;
            }
            var adminRole = "Admin";
            var roleNames = new String[] { adminRole, "Manager", "Crew", "Guest", "Designer" };

            foreach (var roleName in roleNames) {
                var role = await _rolesManager.RoleExistsAsync(roleName);
                if (!role) {
                    var result = await _rolesManager.CreateAsync(new IdentityRole { Name = roleName });

                    _logger.LogInformation("Create {0}: {1}", roleName, result.Succeeded);
                }
            }
            // administrator
            var user = new IdentityUser {
                UserName = Configuration["UserSettings:UserName"],
                Email = Configuration["UserSettings:UserEmail"],
                EmailConfirmed = true
            };
            string userPWD = Configuration["UserSettings:UserPassword"];
            var i = await _userManager.FindByEmailAsync(user.Email);
            if (i == null) {
                var adminUser = await _userManager.CreateAsync(user, userPWD);
                if (adminUser.Succeeded) {
                    await _userManager.AddToRoleAsync(user, adminRole);

                    _logger.LogInformation("Create {0}", user.UserName);
                }
            }
        }
        public void CreateUnits()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            _context.UnitDB.Add(new UnitEntity() { ShortName = "Кг.", LongName = "Киллогам", Weight = 0.001 });
            _context.UnitDB.Add(new UnitEntity() { ShortName = "Гр.", LongName = "Грамм", Weight = 1 });
            _context.UnitDB.Add(new UnitEntity() { ShortName = "Мг.", LongName = "Миллиграм", Weight = 1000 });
            _context.UnitDB.Add(new UnitEntity() { ShortName = "Л.", LongName = "Литров", Weight = 0.001 });
            _context.UnitDB.Add(new UnitEntity() { ShortName = "Мл.", LongName = "Миллилитров", Weight = 1 });
            _context.UnitDB.Add(new UnitEntity() { ShortName = "Шт.", LongName = "Штук", Weight = 1 });
            _context.UnitDB.Add(new UnitEntity() { ShortName = "10 Шт.", LongName = "Штук", Weight = 10 });
            _context.SaveChanges();
        }
    }
}
