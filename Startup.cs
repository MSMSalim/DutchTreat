using System.Reflection;
using System.Text;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add identity and associate with the context
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DutchContext>();


            // Enable cookie for website and JWT token support
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg => {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var issuer = _config["Tokens:Issuer"];
                    var audience = _config["Tokens:Audience"];

                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = key
                    };
                });

            services.AddDbContext<DutchContext>(cfg =>
            {
              
                cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            });

            services.Configure<DutchContext>(o =>
            {
                o.Database.Migrate();
            });

            services.AddControllersWithViews();

            services.AddTransient<DutchSeeder>();
            services.AddTransient<IMailService, NullMailService>();
            services.AddScoped<IDutchRepository, DutchRepository>();

            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Hello my world!!");
                });
            */

            /*
            app.UseDefaultFiles();
            */

            if(env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }
            else
            {
                // Add Error
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            app.UseNodeModules();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(cfg =>
                {
                    cfg.MapControllerRoute("Fallback",
                        "{controller}/{action}/{id?}",
                        new { controller = "App", action = "Index" });
                }
            );

        }
    }
}
