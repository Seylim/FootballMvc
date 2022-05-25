
using Football.Business.Abstract;
using Football.Business.Concrete;
using Football.Business.MapperProfile;
using Football.DataAccess.Data;
using Football.DataAccess.Repositories;
using Football.DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Football.API
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
            services.AddMemoryCache();
            services.AddResponseCaching();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Football.API", Version = "v1" });
            });

            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IClubRepository, EfClubRepository>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<ICoachRepository, EfCoachRepository>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<ILeagueRepository, EfLeagueRepository>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IPlayerRepository, EfPlayerRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, EfUserRepository>();

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            var connectionString = Configuration.GetConnectionString("db");
            services.AddDbContext<FootballDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddAutoMapper(typeof(MapProfile));

            services.AddCors(opt => opt.AddPolicy("Allow", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Burasý çok gizli bölge"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateActor = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,

                            ValidIssuer = "turkcell.com.tr",
                            ValidAudience = "turkcell.com.tr",
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Football.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseResponseCaching();

            app.UseRouting();

            app.UseCors("Allow");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
