using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using System.Linq;
using works.Data;
using works.Features.App.Services;
using works.Features.Identity;

namespace works
{
    public class ThemeState
    {
        public string CurrentTheme { get; set; } = "default";
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", c => c.RequireRole("Admin"));
            });

            services.AddRazorPages();
            services.AddControllers();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddScoped<ThemeState>();
            services.AddScoped<SideMenu>();
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            
            services.AddDistributedMemoryCache();

            services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContextOptions<ApplicationDbContext> identityDbContextOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //EnsureTestUsers(identityDbContextOptions, userManager, roleManager);
            RoleCheck(roleManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        static void RoleCheck(RoleManager<IdentityRole> roleManager)
        {
            var role = roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult();
            if (!role)
                roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter();
        }

        private static void EnsureTestUsers(DbContextOptions<ApplicationDbContext> identityDbContextOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach (var d in userManager.Users.ToList())
            {
                var rst = userManager.DeleteAsync(d);
            }

            //using (var db = new ApplicationDbContext(identityDbContextOptions))
            //{
            //    db.Database.EnsureCreated();
            //}

            //var user = userManager.FindByEmailAsync("user@example.com").GetAwaiter().GetResult();
            //if (user == null)
            //{
            //    userManager.CreateAsync(new IdentityUser("user@example.com") { Email = "user@example.com", EmailConfirmed = true }, "1234").GetAwaiter().GetResult();
            //}

            //var userWithUnconfirmedEmailAddress = userManager.FindByEmailAsync("anotheruser@example.com").GetAwaiter().GetResult();
            //if (userWithUnconfirmedEmailAddress == null)
            //{
            //    userManager.CreateAsync(new IdentityUser("anotheruser@example.com") { Email = "anotheruser@example.com", EmailConfirmed = false }, "1234").GetAwaiter().GetResult();
            //}

            //var admin = userManager.FindByEmailAsync("admin@example.com").GetAwaiter().GetResult();
            //if (admin == null)
            //{
            //    userManager.CreateAsync(new IdentityUser("admin@example.com") { Email = "admin@example.com", EmailConfirmed = true }, "1234").GetAwaiter().GetResult();
            //    admin = userManager.FindByEmailAsync("admin@example.com").GetAwaiter().GetResult();
            //}

            //if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            //{
            //    roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            //}

            //if (!userManager.IsInRoleAsync(admin, "Admin").GetAwaiter().GetResult())
            //{
            //    userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
            //}
        }
    }
}
