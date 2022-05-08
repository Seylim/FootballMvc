using Football.Business.Abstract;
using Football.Business.Concrete;
using Football.Business.MapperProfile;
using Football.DataAccess.Data;
using Football.DataAccess.Repositories;
using Football.DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddScoped<IClubRepository, EfClubRepository>();
            services.AddScoped<ICoachRepository, EfCoachRepository>();
            services.AddScoped<ILeagueRepository, EfLeagueRepository>();
            services.AddScoped<IPlayerRepository, EfPlayerRepository>();
            services.AddScoped<IUserRepository, EfUserRepository>();

            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IUserService, UserService>();

            

            var connectionString = Configuration.GetConnectionString("db");
            services.AddDbContext<FootballDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddAutoMapper(typeof(MapProfile));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(opt =>
                    {
                        opt.LoginPath = "/Users/Login";
                        opt.AccessDeniedPath = "/Users/AccessDeniedPath";
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller=Users}/{action=Profile}/{username?}",
                    defaults: new { controller = "Users", Action = "Profile"});
            });
        }
    }
}
